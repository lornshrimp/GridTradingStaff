using System;
using Lorn.GridTradingStaff.Interfaces.Businesses;
using Lorn.GridTradingStaff.DataModels;
using System.ComponentModel;
using System.Linq.Expressions;
using Lorn.GridTradingStaff;
using Security.DataModels;
using System.Composition;

namespace Lorn.GridTradingStaff.Businesses
{
    public abstract class BusinessBase : Observable
    {
        private Lazy<IBusinessApp> lazyAppService;
        [Import]
        public Lazy<IBusinessApp> LazyAppService
        {
            get => this.lazyAppService;
            set => SetProperty(ref this.lazyAppService, value,
  () =>
  {
      if (this.lazyAppService != null)
      {
          this.lazyAppService.Value.TimerElapsed -= Value_TimerElapsed;
      }
  },
  () =>
  {
      if (this.lazyAppService != null)
      {
          this.lazyAppService.Value.TimerElapsed += Value_TimerElapsed;
      }
  });
        }

        private void Value_TimerElapsed(object sender, EventArgs e)
        {
            TimerElapsed();
        }
        protected virtual void TimerElapsed()
        {

        }

        public IBusinessApp AppService
        {
            get => this.LazyAppService.Value;
        }
    }
}
