using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using Security.DataModels;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb
{
    [Export(typeof(IServiceData<TradeCalendar>))]
    [ExportMetadata("DataProvider", DataProvider.LocalDatabase)]
    [Shared]
    public class LocalTradeCalendarDataService:LocalServiceBase<TradeCalendar>
    {
    }
}
