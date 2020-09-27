using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Composition;
using System.Composition.Hosting;
using System.Reflection;

namespace Lorn.GridTradingStaff.Tests
{
    public class AssembliesLoader
    {
        CompositionHost host;
        public AssembliesLoader(object parent)
        {
            List<Assembly> assemblies = new List<Assembly>()
            {
             Assembly.Load("Lorn.GridTradingStaff.Tests"),
            Assembly.Load("Lorn.GridTradingStaff.DataAdapters.TuShare"),
            Assembly.Load("Lorn.GridTradingStaff.Businesses"),
            Assembly.Load("Lorn.GridTradingStaff.SecurityBusinesses"),
            Assembly.Load("Lorn.GridTradingStaff.DataAdapters.LocalDb"),
                Assembly.Load("Lorn.GridTradingStaff.DataAdapters.LocalDb.Sqlite")
            };
            ContainerConfiguration configuration = new ContainerConfiguration();
            configuration = configuration.WithAssemblies(assemblies);
            host = configuration.CreateContainer();
            host.SatisfyImports(parent);
        }
    }
}
