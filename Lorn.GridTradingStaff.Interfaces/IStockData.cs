using Lorn.GridTradingStaff.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.Interfaces
{
    public interface IStockData
    {
        Task<ICollection<OnsitePrice>> GetDailyPricesAsync(string securityCode, DateTime startDate, DateTime endDate);
    }
}
