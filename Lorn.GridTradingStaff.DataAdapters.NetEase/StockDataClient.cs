using System;
using System.Collections.Generic;
using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Interfaces;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.DataAdapters.NetEase
{
    public class StockDataClient : IStockData
    {
        public async Task<ICollection<OnsitePrice>> GetDailyPricesAsync(string securityCode, DateTime startDate, DateTime endDate)
        {
            HttpClient client = new HttpClient();
            string netEaseCode = securityCode.StartsWith("60") ? "0" + securityCode : "1" + securityCode;
            string requestUrl = "http://quotes.money.163.com/service/chddata.html?code=" + netEaseCode + "&start=" + startDate.ToString("yyyyMMdd") + "&end=" + endDate.ToString("yyyyMMdd") + "&fields=TCLOSE;HIGH;LOW;TOPEN"; //DevSkim: ignore DS137138
            var data = await client.GetAsync(requestUrl);
            var dataString = await data.Content.ReadAsStringAsync();
            var dataEnum = dataString.Split("\r\n");
            List<OnsitePrice> prices = new List<OnsitePrice>();
            var count = dataEnum.Length;
            for (int i = 1; i < count; i++)
            {
                var valueString = dataEnum[i].Split(",");
                var price = new OnsitePrice();
                price.ClosePrice = decimal.Parse(valueString[3]);
                price.HighPrice = decimal.Parse(valueString[4]);
                price.LowPrice = decimal.Parse(valueString[5]);
                price.OpenPrice = decimal.Parse(valueString[6]);
                price.PriceTime = DateTime.Parse(valueString[0]);
                prices.Add(price);
            }
            return prices;
        }
    }
}
