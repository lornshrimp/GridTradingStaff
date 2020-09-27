using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System.Threading;
using Lorn.GridTradingStaff.Interfaces.Businesses;

namespace Lorn.GridTradingStaff.Tests.Businesses
{
    [TestClass]
    public class TestAppService:TestBase<IBusinessApp>
    {
        int times = 0;

        [TestMethod]
        public void TestAppServiceRun()
        {
            Assert.IsInstanceOfType(TestObject, typeof(IBusinessApp));
            TestObject.TimerElapsed += TestObject_TimerElapsed;
            this.TestObject.InitService();
            Thread.Sleep(12000);
            Assert.IsTrue(times > 10);
        }

        private void TestObject_TimerElapsed(object sender, EventArgs e)
        {
            times++;
        }
    }
}
