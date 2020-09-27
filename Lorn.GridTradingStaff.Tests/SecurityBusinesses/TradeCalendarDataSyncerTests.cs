using Lorn.GridTradingStaff.Businesses;
using Lorn.GridTradingStaff.Interfaces.Businesses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.Tests.SecurityBusinesses
{
    [TestClass]
    public class TradeCalendarDataSyncerTests: TestBase<ISyncTradeCalendar>
    {
        [TestMethod()]
        public async Task GetDataAsyncTest()
        {
            await this.AppManager.InitService();
            Thread.Sleep(1000);
            var data = await TestObject.GetDataAsync(null);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count() > 1000);
            Trace.Write(data.Count());
        }

        [TestMethod()]
        public async Task RefreshDataAsyncTest()
        {
            await this.AppManager.InitService();
            Thread.Sleep(1000);
            await (TestObject as DataBusinessBase<TradeCalendar>).RefreshDataAsync();

        }
    }
}
