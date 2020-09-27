using Lorn.GridTradingStaff.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb.Sqlite
{
    [Export(typeof(IInitService))]
    [ExportMetadata("DataProvider", DataProvider.LocalDatabase)]
    [Shared]
    public class ServiceManager : IInitService
    {
        private bool initCompleted = false;
        [Import]
        public Lazy<LocalDataContext> DataContext
        {
            get; set;
        }

        public bool InitCompleted => this.initCompleted;

        public async Task<bool> InitServiceContextAsync()
        {
            await DataContext.Value.Database.MigrateAsync();
            this.initCompleted = true;
            return true;
        }
    }
}
