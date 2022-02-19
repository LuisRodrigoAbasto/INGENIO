using Abasto.Library.DevExtreme.Config;
using Abasto.Library.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Abasto.Library.DevExtreme
{
    public static class PaginateExtensions
    {
        public static  async Task<IPaginateResult<T>> PaginateResultAsync<T>(this IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            return await PageResultAsync<T>(source: source, filter: filter, options: options);
        }
        public static  IPaginateResult<T> PaginateResult<T>(this IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            return PageResult<T>(source: source, filter: filter,  options: options);
        }
        public static async Task<IPaginateResult<T>> PaginateResultAsync<T>(this IQueryable<T> source, Action<QueryFilter> options) where T : class
        {
            return await PageResultAsync<T>(source: source, filter: null,  options: options);
        }
        public static IPaginateResult<T> PaginateResult<T>(this IQueryable<T> source, Action<QueryFilter> options) where T : class
        {
            return PageResult<T>(source: source, filter: null,  options: options);
        }
        public static async Task<IPaginateResult<T>> PaginateResultAsync<T>(this IQueryable<T> source, string filter) where T : class
        {
            return await PageResultAsync<T>(source: source, filter: filter,  options: null);
        }
        public static IPaginateResult<T> PaginateResult<T>(this IQueryable<T> source, string filter) where T : class
        {
            return PageResult<T>(source: source, filter: filter,  options: null);
        }
        public static async Task<IPaginateResult<T>> PaginateResultAsync<T>(this IQueryable<T> source) where T : class
        {
            return await PageResultAsync<T>(source: source, filter: null,  options: null);
        }
        public static IPaginateResult<T> PaginateResult<T>(this IQueryable<T> source) where T : class
        {
            return PageResult<T>(source: source, filter: null, options: null);
        }
        private static Task<IPaginateResult<T>> PageResultAsync<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
       {
            IPaginate paginate = (source, filter,true, options) as IPaginate;
            return paginate.PaginateResultAsync<T>();
    }
        private static IPaginateResult<T> PageResult<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            IPaginate paginate = (source, filter, false, options) as IPaginate;
            return paginate.PaginateResult<T>();
        }

    }
}