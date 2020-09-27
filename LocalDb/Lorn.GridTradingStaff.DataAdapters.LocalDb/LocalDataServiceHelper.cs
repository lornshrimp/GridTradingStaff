using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Security.DataModels;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb
{
    public static class LocalServiceHelper
    {
        public static async Task<int> UpdateDataAsync<T>(this LocalDataContext context, IEnumerable<T> datas) where T : DataBase
        {
            int result = 0;
            while (context.ContextLocking == true)
            {
                Thread.Sleep(100);
            }
            try
            {
                context.ContextLocking = true;
                var ts = context.Set<T>();
                bool saveData = false;
                foreach (var dataItem in datas.Where(o => o.Deleted == false || o.UpdateTime != DateTimeOffset.MinValue))
                {
                    if (dataItem.Validate().Count == 0)
                    {
                        var oldUpdateTime = dataItem.UpdateTime;
                        try
                        {
                            var oldDataItem = ts.FirstOrDefault(o => o.Id == dataItem.Id);
                            if (oldDataItem != null)
                            {
                                dataItem.UpdateTime = DateTimeOffset.UtcNow;
                                oldDataItem.RefreshData(dataItem);
                                ts.Update(oldDataItem);
                                saveData = true;
                            }
                            else
                            {
                                if (dataItem.Deleted == false)
                                {
                                    dataItem.UpdateTime = DateTimeOffset.UtcNow;
                                    ts.Add(dataItem);
                                    saveData = true;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dataItem.UpdateTime = oldUpdateTime;
                            throw e;
                        }
                    }
                }
                if (saveData == true)
                {
                    result = await context.SaveChangesAsync();
                }
                else
                {
                    result = 0;
                }
                context.ContextLocking = false;
            }
            catch(Exception ex)
            {
                context.ContextLocking = false;
                throw ex;
            }
            return result;
        }
        public static IQueryable<T> GetData<T>(this DbSet<T> ts, Expression<Func<T, bool>> expression,Expression<Func<T, bool>> unDeleteExpression, DateTimeOffset? lastRefreshTime = null, DateTimeOffset? minUpdateTime = null) where T : DataBase
        {
            var query = ts.Where(unDeleteExpression);
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (lastRefreshTime != null && minUpdateTime != null)
            {
                query = query.Where(o => o.UpdateTime >= lastRefreshTime || o.UpdateTime <= minUpdateTime);
            }
            else if (lastRefreshTime != null)
            {
                query = query.Where(o => o.UpdateTime >= lastRefreshTime);
            }
            else if (minUpdateTime != null)
            {
                query = query.Where(o => o.UpdateTime <= minUpdateTime);
            }
            return query;
        }
    }
}
