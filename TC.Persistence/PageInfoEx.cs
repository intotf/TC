using Infrastructure.Page;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Persistence
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public static class PageInfoEx
    {
        /// <summary>
        /// 执行分页        
        /// 性能比较好
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>    
        /// <param name="orderBy">排序字符串</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public static async Task<PageInfo<T>> ToPageAsync<T>(this IQueryable<T> source, string orderBy, int pageIndex, int pageSize) where T : class
        {
            int total = await source.CountAsync();
            var inc = total % pageSize > 0 ? 0 : -1;
            var maxPageIndex = (int)Math.Floor((double)total / pageSize) + inc;
            pageIndex = Math.Max(0, Math.Min(pageIndex, maxPageIndex));

            var data = await source.OrderBy(orderBy).ToListAsync();
            var page = new PageInfo<T>(total, data) { PageIndex = pageIndex, PageSize = pageSize };
            return page;
        }

        /// <summary>
        /// 执行分页    
        /// 性能比较差，不推荐使用
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>    
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [Obsolete("请使用ToPageAsync(orderBy, pageIndex, pageSize)方法替代", false)]
        public static async Task<PageInfo<T>> ToPageAsync<T>(this IOrderedQueryable<T> source, int pageIndex, int pageSize) where T : class
        {
            int total = await source.CountAsync();
            var inc = total % pageSize > 0 ? 0 : -1;
            var maxPageIndex = (int)Math.Floor((double)total / pageSize) + inc;
            pageIndex = Math.Max(0, Math.Min(pageIndex, maxPageIndex));

            var data = await source.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            var page = new PageInfo<T>(total, data) { PageIndex = pageIndex, PageSize = pageSize };
            return page;
        }
    }
}
