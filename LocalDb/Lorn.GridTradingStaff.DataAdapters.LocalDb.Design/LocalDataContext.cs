using System;
using System.Collections.Generic;
using System.Text;
#if NETSTANDARD
using System.Composition;
using Lorn.GridTradingStaff.Interfaces.Services;
using Lorn.GridTradingStaff.Interfaces;
#endif
using Microsoft.EntityFrameworkCore;
using Security.DataModels;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Lorn.GridTradingStaff.DataModels;
using System.Data;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb
{
    public abstract class LocalDataContext : DbContext
    {
#if NETSTANDARD
        [ImportMany]
        public IEnumerable<Lazy<IServiceInfo, MetaData>> ServiceInfos
        {
            get; set;
        }
#endif
        public DbSet<ChinaStock> ChinaStocks { get; set; }
        public DbSet<SecurityProviderInfo> SecurityProviderInfos { get; set; }
        public DbSet<TradeCalendar> TradeCalendars { get; set; }
        private bool contextLocking = false;
        public bool ContextLocking { get => contextLocking; set => contextLocking = value; }

        protected string DatabaseFilePath
        {
            get
            {
#if NETSTANDARD
                return ServiceInfos.First(o => o.Metadata.DataProvider == DataProvider.LocalDatabase).Value.ContextPath;
#else
                return "";
#endif
            }
        }
    }
}
