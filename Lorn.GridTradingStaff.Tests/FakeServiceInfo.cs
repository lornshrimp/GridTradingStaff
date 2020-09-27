using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using Lorn.GridTradingStaff.Interfaces;

namespace Lorn.GridTradingStaff.Tests
{
    [Export(typeof(IServiceInfo))]
    [ExportMetadata("DataProvider", DataProvider.LocalDatabase)]
    [Shared]
    public class FakeServiceInfo : IServiceInfo
    {
        public string ContextPath => AppContext.BaseDirectory+"\\";
    }
}
