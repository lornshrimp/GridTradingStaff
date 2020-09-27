using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.Interfaces.Services
{
    /// <summary>
    /// 服务初始化接口
    /// </summary>
    public interface IInitService
    {
        /// <summary>
        /// 初始化服务上下文
        /// </summary>
        /// <returns>初始化是否成功</returns>
        Task<bool> InitServiceContextAsync();
        bool InitCompleted { get; }
    }
}
