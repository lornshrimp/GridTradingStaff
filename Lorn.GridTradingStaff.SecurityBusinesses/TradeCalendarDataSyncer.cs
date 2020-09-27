using Lorn.GridTradingStaff.Businesses;
using Lorn.GridTradingStaff.Interfaces.Businesses;
using Lorn.GridTradingStaff.Interfaces.Services;
using Security.DataModels;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.SecurityBusinesses
{
    [Export(typeof(ISyncTradeCalendar))]
    [Shared]
    public class TradeCalendarDataSyncer : DataBusinessBase<TradeCalendar>, ISyncTradeCalendar
    {
        [Import]
        public Lazy<ILoadBasicSecurityInfo> LazyBasicSecurityInfoAdapter { get; set; }
        public ILoadBasicSecurityInfo BasicSecurityInfoAdapter { get => this.LazyBasicSecurityInfoAdapter.Value; }

        [Import]
        public Lazy<IServiceData<TradeCalendar>> LazyTradeCalendarLocalService { get; set; }
        public IServiceData<TradeCalendar> TradeCalendarLocalService { get => LazyTradeCalendarLocalService.Value; }

        protected override TimeSpan RefreshDataTimeSpan => new TimeSpan(1, 0, 0, 0);

        public async Task<IEnumerable<TradeCalendar>> GetDataAsync(Expression<Func<TradeCalendar, bool>> expression = null, DateTimeOffset? lastRefreshTime = null, DateTimeOffset? minUpdateTime = null)
        {
            return await this.TradeCalendarLocalService.GetDataAsync(expression, this.UnDeletedExpression, lastRefreshTime, minUpdateTime);
        }

        protected override async Task RefreshDataAsyncInternal()
        {
            //获取过去5年及未来1年的交易日历
            var startDate = DateTime.Now.AddYears(-5);
            var endDate = DateTime.Now.AddYears(1);
            var calendarFromWeb = this.BasicSecurityInfoAdapter.GetTradeCalendars(Exchange.SSE,startDate,endDate).Result.ToList();
            calendarFromWeb.AddRange(this.BasicSecurityInfoAdapter.GetTradeCalendars(Exchange.SZSE, startDate, endDate).Result.ToList());

            var calendarsOld = this.TradeCalendarLocalService.GetDataAsync(o=>o.CalendarDate >= startDate && o.CalendarDate <= endDate, this.UnDeletedExpression).Result;
            foreach (var calendarWeb in calendarFromWeb)
            {
                var calendarOld = calendarsOld.FirstOrDefault(o => o.CalendarDate == calendarWeb.CalendarDate && o.Exchange == calendarWeb.Exchange);
                if (calendarOld != null)
                {
                    calendarWeb.Id = calendarOld.Id;
                }
                else calendarWeb.Id = Guid.NewGuid();
            }
            await this.TradeCalendarLocalService.SaveDataAsync(calendarFromWeb);
            this.RaiseDataRefreshed(calendarFromWeb);
        }
    }
}
