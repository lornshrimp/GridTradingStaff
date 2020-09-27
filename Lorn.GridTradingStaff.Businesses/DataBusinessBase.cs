using System;
using System.Collections.Generic;
using System.Text;
using Lorn.GridTradingStaff.Interfaces.Businesses;
using System.Composition;
using System.Threading.Tasks;
using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using System.Linq.Expressions;
using Security.DataModels;
using System.Linq;

namespace Lorn.GridTradingStaff.Businesses
{
    public abstract class DataBusinessBase<TData> : BusinessBase where TData : DataBase
    {
        public event EventHandler<IEnumerable<TData>> DataRefreshed;

        protected virtual TimeSpan RefreshDataTimeSpan => new TimeSpan(0, 0, 1);
        private DateTime lastRefreshDataTime = DateTime.MinValue;

        protected virtual Expression<Func<TData, bool>> UnDeletedExpression => o => o.Deleted == false;
        protected bool DataRefreshing { get; set; }

        protected async override void TimerElapsed()
        {
            base.TimerElapsed();
            if (DateTime.Now > lastRefreshDataTime + RefreshDataTimeSpan && DataRefreshing == false)
            {
                try
                {
                    await RefreshDataAsync();
                    this.lastRefreshDataTime = DateTime.Now;
                }
                catch (Exception ex)
                {
                    DataRefreshing = false;
                    throw ex;
                }
            }
        }
        public async Task RefreshDataAsync()
        {
            if (DataRefreshing == false)
            {
                try
                {
                    DataRefreshing = true;
                    await RefreshDataAsyncInternal();
                    DataRefreshing = false;
                }
                catch (Exception ex)
                {
                    DataRefreshing = false;
                    throw ex;
                }
            }
        }

        protected abstract Task RefreshDataAsyncInternal();

        protected virtual void RaiseDataRefreshed(IEnumerable<TData> datas)
        {
            if (this.DataRefreshed != null)
            {
                this.DataRefreshed(this, datas);
            }
        }
    }

}
