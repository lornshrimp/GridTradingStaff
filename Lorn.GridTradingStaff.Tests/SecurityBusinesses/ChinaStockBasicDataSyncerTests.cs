using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lorn.GridTradingStaff.SecurityBusinesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Lorn.GridTradingStaff.Tests;
using NuGet.Frameworks;
using Lorn.GridTradingStaff.Interfaces.Businesses;
using System.Threading;
using Lorn.GridTradingStaff.Businesses;
using Security.DataModels;
using System.Diagnostics;

namespace Lorn.GridTradingStaff.SecurityBusinesses.Tests
{
    [TestClass()]
    public class ChinaStockBasicDataSyncerTests : TestBase<ISyncChinaStockBasicData>
    {
        [TestMethod]
        public void BusinessInitTest()
        {
            this.AppManager.InitService();
            Assert.IsNotNull(TestObject);
            Assert.IsNotNull((TestObject as DataBusinessBase<ChinaStock>).AppService);
        }
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
            await (TestObject as DataBusinessBase<ChinaStock>).RefreshDataAsync();
           
        }
    }
}