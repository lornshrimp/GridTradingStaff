using System;
using Lorn.GridTradingStaff.Interfaces.Services;
using Lorn.GridTradingStaff.Interfaces.Businesses;
using System.Composition;
using System.Collections.Generic;
using Security.DataModels;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Businesses;
namespace Lorn.GridTradingStaff.SecurityBusinesses
{
    [Export(typeof(ISyncChinaStockBasicData))]
    [Shared]
    public class ChinaStockBasicDataSyncer : DataBusinessBase<ChinaStock>, ISyncChinaStockBasicData
    {
        [Import]
        public Lazy<ILoadBasicSecurityInfo> LazyBasicSecurityInfoAdapter { get; set; }
        public ILoadBasicSecurityInfo BasicSecurityInfoAdapter { get => this.LazyBasicSecurityInfoAdapter.Value; }
        [Import]
        public Lazy<IServiceData<ChinaStock>> LazyChinaStockLocalService { get; set; }
        public IServiceData<ChinaStock> ChinaStockLocalService { get => LazyChinaStockLocalService.Value; }

        [Import]
        public Lazy<IServiceData<SecurityProviderInfo>> LazySecurityProviderInfoLocalService { get; set; }
        public IServiceData<SecurityProviderInfo> SecurityProviderInfoLocalService { get => LazySecurityProviderInfoLocalService.Value; }


        protected override TimeSpan RefreshDataTimeSpan => new TimeSpan(1, 0, 0, 0);

        public async Task<IEnumerable<ChinaStock>> GetDataAsync(Expression<Func<ChinaStock, bool>> expression = null, DateTimeOffset? lastRefreshTime = null, DateTimeOffset? minUpdateTime = null)
        {
            return await this.ChinaStockLocalService.GetDataAsync(expression, this.UnDeletedExpression, lastRefreshTime, minUpdateTime);
        }

        protected override async Task RefreshDataAsyncInternal()
        {
            var chinaStocksNewPair = new KeyValuePair<List<ChinaStock>, List<SecurityProviderInfo>>(new List<ChinaStock>(), new List<SecurityProviderInfo>());
            var chinaStocksListing = await BasicSecurityInfoAdapter.GetChinaStocksAsync(stockStatus: StockStatus.Listing);
            var chinaStocksPause = await BasicSecurityInfoAdapter.GetChinaStocksAsync(stockStatus: StockStatus.Pause);
            var chinaStocksDelisting = await BasicSecurityInfoAdapter.GetChinaStocksAsync(stockStatus: StockStatus.Delisting);

            chinaStocksNewPair.Key.AddRange(chinaStocksListing.Key);
            chinaStocksNewPair.Key.AddRange(chinaStocksPause.Key);
            chinaStocksNewPair.Key.AddRange(chinaStocksDelisting.Key);
            chinaStocksNewPair.Value.AddRange(chinaStocksListing.Value);
            chinaStocksNewPair.Value.AddRange(chinaStocksPause.Value);
            chinaStocksNewPair.Value.AddRange(chinaStocksDelisting.Value);

            var chinaStocksOld = await ChinaStockLocalService.GetDataAsync(null,this.UnDeletedExpression);
            var SecurityProviderInfosOld = await SecurityProviderInfoLocalService.GetDataAsync(null,o =>o.Deleted == false);
            foreach (var chinaStock in chinaStocksNewPair.Key)
            {
                var chinaStockOld = chinaStocksOld.FirstOrDefault(o => o.Code == chinaStock.Code);
                if (chinaStockOld != null)
                {
                    chinaStock.Id = chinaStockOld.Id;
                }
                else chinaStock.Id = Guid.NewGuid();
                var dataProviderInfo = chinaStocksNewPair.Value.First(o => o.Security == chinaStock);
                var dataProviderInfoOld = SecurityProviderInfosOld.FirstOrDefault(o => o.Provider == dataProviderInfo.Provider && o.SecurityId == chinaStock.Id);
                if (dataProviderInfoOld != null)
                {
                    dataProviderInfo.Id = dataProviderInfoOld.Id;
                }
                else dataProviderInfo.Id = Guid.NewGuid();
                dataProviderInfo.SecurityId = chinaStock.Id;
            }
            await ChinaStockLocalService.SaveDataAsync(chinaStocksNewPair.Key);
            await SecurityProviderInfoLocalService.SaveDataAsync(chinaStocksNewPair.Value);
            this.RaiseDataRefreshed(chinaStocksNewPair.Key);
        }
    }
}
