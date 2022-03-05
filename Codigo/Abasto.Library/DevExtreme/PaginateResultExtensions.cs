using Abasto.Library.DevExtreme.Config;
using Abasto.Library.Interfaces;
using System;
using System.Data.Entity.Infrastructure;
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
        private static Task<IPaginateResult<T>> PageResultAsync<T>(this IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
       {
            IPaginateQuery<T> paginate = new PaginateQuery<T>(source,filter,options);
            IPaginate<T> result = paginate as IPaginate<T>;
            if (result!=null)return result.PaginateResultAsync<T>();
            throw new Exception("<" + typeof(T)?.ToString() + ">");
        }
        private static IPaginateResult<T> PageResult<T>(this IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            IPaginateQuery<T> paginate = new PaginateQuery<T>(source, filter, options);
            IPaginate<T> result = paginate as IPaginate<T>;
            if (result != null) return result.PaginateResult<T>();
            throw new Exception("<" + typeof(T)?.ToString() + ">");
        }

    }
}