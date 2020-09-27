using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;

namespace Lorn.GridTradingStaff.Tests
{
    [Export(typeof(IServiceInfo))]
    [ExportMetadata("DataProvider", DataProvider.TuShare)]
    [Shared]
    public class FakeServiceInfoTuShare : IServiceInfo
    {
        public string ContextPath => AppContext.BaseDirectory+"\\";
    }
}
