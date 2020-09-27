using System;
using System.Collections.Generic;
using System.Text;
using Lorn.GridTradingStaff.DataModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using Security.DataModels;


namespace Lorn.GridTradingStaff.Interfaces.Businesses
{
    /// <summary>
    /// 数据获取接口
    /// </summary>
    /// <typeparam name="TData">获取的数据类型</typeparam>
    public interface IGetData<TData> where TData : DataBase
    {
        /// <summary>
        /// 异步获取数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="lastRefreshTime">上次刷新时间</param>
        /// <param name="minUpdateTime">最早的更新时间</param>
        /// <returns>获取到的数据</returns>
        Task<IEnumerable<TData>> GetDataAsync(Expression<Func<TData, bool>> expression = null, DateTimeOffset? lastRefreshTime = null, DateTimeOffset? minUpdateTime = null);
    }
    /// <summary>
    /// 数据存储接口
    /// </summary>
    /// <typeparam name="TData">存储的数据类型</typeparam>
    public interface ISaveData<TData> where TData : DataBase
    {
        /// <summary>
        /// 异步保存数据
        /// </summary>
        /// <param name="data">要保存的数据</param>
        /// <returns>异步任务</returns>
        Task SaveDataAsync(TData data);
        /// <summary>
        /// 正在创建数据
        /// </summary>
        event EventHandler<TData> DataCreating;
        /// <summary>
        /// 数据已保存
        /// </summary>
        event EventHandler<TData> DataSaved;
        /// <summary>
        /// 数据已删除
        /// </summary>
        event EventHandler<TData> DataDeleted;
        /// <summary>
        /// 坚持是否拥有修改权限
        /// </summary>
        /// <param name="data">要检查权限的数据</param>
        /// <returns>是否拥有权限</returns>
        bool CheckModifyDataRight(TData data);
        /// <summary>
        /// 检查是否拥有删除权限
        /// </summary>
        /// <param name="data">要检查权限的数据</param>
        /// <returns>是否拥有权限</returns>
        bool CheckDeleteDataRight(TData data);
        /// <summary>
        /// 检查是否拥有添加权限
        /// </summary>
        /// <returns>是否拥有权限</returns>
        bool CheckAddDataRight();
        /// <summary>
        /// 创建一条数据
        /// </summary>
        /// <returns>创建的数据</returns>
        TData AddData();
    }
}
