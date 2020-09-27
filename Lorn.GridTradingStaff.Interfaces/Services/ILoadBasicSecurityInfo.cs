using Security.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lorn.GridTradingStaff.DataModels;

namespace Lorn.GridTradingStaff.Interfaces.Services
{
    /// <summary>
    /// 基础证券信息同步接口
    /// </summary>
    public interface ILoadBasicSecurityInfo
    {
        /// <summary>
        /// 异步获取中国股票基本信息
        /// </summary>
        /// <param name="exchange">交易所代码</param>
        /// <param name="stockStatus"'>上市状态</param>
        /// <returns>中国股票基本信息集合</returns>
        Task<KeyValuePair<IEnumerable<ChinaStock>, IEnumerable<SecurityProviderInfo>>> GetChinaStocksAsync(Exchange? exchange = null, StockStatus stockStatus = StockStatus.Listing);
        /// <summary>
        /// 异步获取交易日历
        /// </summary>
        /// <param name="exchange">交易所代码</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>交易日历</returns>
        Task<IEnumerable<TradeCalendar>> GetTradeCalendars(Exchange exchange = Exchange.SSE, DateTime? startDate = null, DateTime? endDate = null);
    }
}
