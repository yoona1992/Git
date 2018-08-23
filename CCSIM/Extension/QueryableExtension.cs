using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Extension
{
    public static class QueryableExtension
    {
        /// <summary>
        /// 分页查询扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            return query.Skip(skipCount).Take(maxResultCount);
        }
        public static IQueryable<T> Pagination<T, TKey>(this IQueryable<T> list, Expression<Func<T, TKey>> order, int page, int size, out int count, Expression<Func<T, bool>> whereLambda = null)
        {
            list = list.OrderBy(order);
            if (whereLambda != null)
            {
                list = list.Where(whereLambda);
            }
            count = list.Count();
            return list.Skip((page - 1) * size).Take(size);
        }

        /// <summary>
        /// 查询扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> Select<T>(this IQueryable query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            return query.ProjectTo<T>();
        }
    }
}
