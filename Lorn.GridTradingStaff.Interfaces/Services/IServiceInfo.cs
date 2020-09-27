using System;
using System.Collections.Generic;
using System.Text;

namespace Lorn.GridTradingStaff.Interfaces.Services
{
    /// <summary>
    /// 服务信息接口
    /// </summary>
    public interface IServiceInfo
    {
        /// <summary>
        /// 数据存储上下文
        /// </summary>
        string ContextPath { get; }
    }
}
