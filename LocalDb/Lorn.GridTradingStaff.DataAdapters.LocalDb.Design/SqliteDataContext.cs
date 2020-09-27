using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using System.Threading;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb
{
#if NETSTANDARD
    [Export(typeof(LocalDataContext))]
#endif
    public class SqliteDataContext:LocalDataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=" + this.DatabaseFilePath + "SecurityData.db");
        }
    }
}
