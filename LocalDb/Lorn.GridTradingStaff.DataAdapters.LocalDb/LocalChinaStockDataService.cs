using System;
using System.Collections.Generic;
using System.Text;
using System.Composition;
using Security.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using Lorn.GridTradingStaff.DataModels;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb
{
    [Export(typeof(IServiceData<ChinaStock>))]
    [ExportMetadata("DataProvider", DataProvider.LocalDatabase)]
    [Shared]
    public class LocalChinaStockDataService : LocalServiceBase<ChinaStock>
    {
    }
}
