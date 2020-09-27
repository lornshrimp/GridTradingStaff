using Lorn.GridTradingStaff.Interfaces.Businesses;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.Tests
{
    public abstract class TestBase<TTestObject>
    {
        AssembliesLoader loader;
        public TestBase()
        {
            loader = new AssembliesLoader(this);
        }

        [Import]
        public IBusinessApp AppManager { get; set; }

        private Lazy<TTestObject> lazyTestObject;
        [Import]
        public Lazy<TTestObject> LazyTestObject
        {
            get
            {
                return this.lazyTestObject;
            }
            set
            {
                this.lazyTestObject = value;
            }
        }
        public TTestObject TestObject => this.LazyTestObject.Value;
    }
}
