using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lorn.GridTradingStaff.DataAdapters.TuShare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lorn.GridTradingStaff.DataModels;

namespace Lorn.GridTradingStaff.DataAdapters.TuShare.Tests
{
    [TestClass()]
    public class SecurityBasicInfoAdapterTests
    {
        [TestMethod()]
        public void GetChinaStocksAsyncTest()
        {
            SecurityBasicInfoAdapter securityBasicInfoAdapter = new SecurityBasicInfoAdapter();
            var data = securityBasicInfoAdapter.GetChinaStocksAsync().Result;
            Assert.AreEqual(data.Key.Count(), data.Value.Count());
            foreach (var stock in data.Key)
            {
                Assert.AreEqual(1, data.Value.Count(o => o.ProviderKey == stock.ProviderKey));
                var securityProviderInfo = data.Value.First(o => o.ProviderKey == stock.ProviderKey);
                Assert.AreEqual(DataProvider.TuShare, securityProviderInfo.Provider);
            }
        }
        [TestMethod]
        public void GetTradeCalendarAsyncTest()
        {
            SecurityBasicInfoAdapter securityBasicInfoAdapter = new SecurityBasicInfoAdapter();
            var data = securityBasicInfoAdapter.GetTradeCalendars().Result;
            Assert.IsTrue(data.Count() > 0);
        }
    }
}