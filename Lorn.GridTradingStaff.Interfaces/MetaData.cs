using System;
using System.Collections.Generic;
using System.Text;
using Lorn.GridTradingStaff.DataModels;
using Security.DataModels;

namespace Lorn.GridTradingStaff.Interfaces
{
    /// <summary>
    /// 服务元数据
    /// </summary>
    public class MetaData
    {
        /// <summary>
        /// 数据提供方
        /// </summary>
        public DataProvider DataProvider { get; set; }
    }
}
