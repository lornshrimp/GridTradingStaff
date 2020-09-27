using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Linq.Expressions;
using Security.DataModels;
using Lorn.GridTradingStaff.Interfaces.Services;
using System.Threading;
using System.Linq;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb
{
    public abstract class LocalServiceBase<TData> : IServiceData<TData> where TData : DataBase
    {
        [Import]
        public Lazy<LocalDataContext> DataContext
        {
            get; set;
        }
        public virtual async Task<IEnumerable<TData>> GetDataAsync(Expression<Func<TData, bool>> expression, Expression<Func<TData, bool>> unDeleteExpression, DateTimeOffset? lastRefreshTime = null, DateTimeOffset? minUpdateTime = null)
        {
            IEnumerable<TData> datas = null;
            while (DataContext.Value.ContextLocking == true)
            {
                Thread.Sleep(100);
            }
            try
            {
                DataContext.Value.ContextLocking = true;
                var query = DataContext.Value.Set<TData>().GetData(expression, unDeleteExpression, lastRefreshTime, minUpdateTime);
                var data = await query.ToListAsync();
                datas = data.OrderByDescending(o => o.UpdateTime);
                DataContext.Value.ContextLocking = false;
            }
            catch(Exception ex)
            {
                DataContext.Value.ContextLocking = false;
                throw ex;
            }
            return datas;

        }

        public virtual async Task<int> SaveDataAsync(TData data) => await this.SaveDataAsync(new List<TData>() { data });

        public virtual async Task<int> SaveDataAsync(IEnumerable<TData> data)
        {
            return await DataContext.Value.UpdateDataAsync<TData>(data);
        }
    }
}
