using System;
using System.Collections.Generic;
using System.Text;
using Lorn.GridTradingStaff.Interfaces.Businesses;
using System.Composition;
using Security.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using System.Timers;
using System.Threading.Tasks;
using Lorn.GridTradingStaff.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading;
using Timer = System.Timers.Timer;

namespace Lorn.GridTradingStaff.Businesses
{
    [Export(typeof(IBusinessApp))]
    [Shared]
    public class AppService : Observable, IBusinessApp
    {
        private Timer timer;
        private bool initCompleted = false;

        [ImportMany]
        public IEnumerable<Lazy<IInitService, MetaData>> ServiceContexts { get; set; }

        public bool InitCompleted => this.initCompleted;

        public event EventHandler TimerElapsed;

        public AppService()
        {
            timer = new Timer();
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();
            this.RaiseTimerElapsed();
        }

        protected virtual void RaiseTimerElapsed()
        {
            if (this.TimerElapsed != null)
            {
                this.TimerElapsed(this, new EventArgs());
            }
        }

        protected virtual void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RaiseTimerElapsed();
        }

        public async Task<bool> InitService()
        {
            foreach (var service in ServiceContexts)
            {
                Task.Run(()=> service.Value.InitServiceContextAsync());
            }
            Task.Run(() => CheckServiceInitStatus());
            return true;
        }
        private async Task CheckServiceInitStatus()
        {
            while (ServiceContexts.Any(o => o.Value.InitCompleted == false))
            {
                Thread.Sleep(100);
            }
            this.initCompleted = true;
        }
    }
}
