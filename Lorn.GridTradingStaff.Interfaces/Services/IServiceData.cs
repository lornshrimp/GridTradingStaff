using Security.DataModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lorn.GridTradingStaff.Interfaces.Services
{
    /// <summary>
    /// 数据服务接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IServiceData<T> where T : DataBase
    {
        /// <summary>
        /// 异步获取数据
        /// </summary>
        /// <param name="expression">数据表达式</param>
        /// <param name="unDeleteExpression">非删除数据表达式</param>
        /// <param name="lastRefreshTime">上次刷新时间</param>
        /// <param name="minUpdateTime">最小的数据更新时间</param>
        /// <returns>获取到的数据</returns>
        Task<IEnumerable<T>> GetDataAsync(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> unDeleteExpression, DateTimeOffset? lastRefreshTime = null, DateTimeOffset? minUpdateTime = null);
        /// <summary>
        /// 异步保存数据
        /// </summary>
        /// <param name="data">要保存的数据</param>
        /// <returns>保存成功的数据数量</returns>
        Task<int> SaveDataAsync(T data);
        /// <summary>
        /// 异步保存数据
        /// </summary>
        /// <param name="data">要保存的数据</param>
        /// <returns>保存成功的数据数量</returns>
        Task<int> SaveDataAsync(IEnumerable<T> data);
    }
}
