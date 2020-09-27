using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.Interfaces.Businesses
{
    /// <summary>
    /// 刷新数据事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void RefreshDataEventHandler(object sender, EventArgs args);
    /// <summary>
    /// 业务服务基础接口
    /// </summary>
    public interface IBusinessApp : INotifyPropertyChanged
    {
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <returns>初始化是否成功</returns>
        Task<bool> InitService();
        bool InitCompleted { get; }
        event EventHandler TimerElapsed;
    }
}
