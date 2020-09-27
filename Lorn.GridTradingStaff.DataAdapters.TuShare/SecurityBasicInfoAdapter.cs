using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using Security.DataModels;
using TuSharePro.Models;
using TuSharePro;
using System.Collections.ObjectModel;
using System.Composition;

namespace Lorn.GridTradingStaff.DataAdapters.TuShare
{
    [Shared]
    [Export(typeof(ILoadBasicSecurityInfo))]
    [ExportMetadata("DataProvider", DataProvider.TuShare)]
    public class SecurityBasicInfoAdapter : AdapterBase, ILoadBasicSecurityInfo
    {
        public async Task<KeyValuePair<IEnumerable<ChinaStock>, IEnumerable<SecurityProviderInfo>>> GetChinaStocksAsync(Exchange? exchange = null, StockStatus stockStatus = StockStatus.Listing)
        {
            CoreApi client = new CoreApi
            {
                Token = token
            };
            var response = client.stock_basic(null,stockStatus,exchange);
            if (response.Code == 0)
            {
                var stocks = response.ChinaStocks;
                ObservableCollection<SecurityProviderInfo> securityProviderInfos = new ObservableCollection<SecurityProviderInfo>();
                foreach (var stock in stocks)
                {
                    securityProviderInfos.Add(new SecurityProviderInfo() { Deleted = false, Id = Guid.Empty, Provider = DataProvider.TuShare, ProviderKey = stock.ProviderKey, Security = stock });
                }
                return new KeyValuePair<IEnumerable<ChinaStock>, IEnumerable<SecurityProviderInfo>>(stocks, securityProviderInfos);
            }
            else
            {
                throw new Exception(response.Message.ToString());
            }
        }

        public async Task<IEnumerable<TradeCalendar>> GetTradeCalendars(Exchange exchange = Exchange.SSE, DateTime? startDate = null, DateTime? endDate = null)
        {
            CoreApi client = new CoreApi
            {
                Token = token
            };
            var response = client.trade_cal(exchange, startDate, endDate);
            return response.TradeCalendars;
        }
    }
}
