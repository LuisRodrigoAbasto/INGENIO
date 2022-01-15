using Abasto.Library.DevExtreme.Config;
using Abasto.Library.General;
using Abasto.Library.Interfaces;
using Abasto.Library.Property;
using Abasto.Library.Property.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Abasto.Library.DevExtreme
{
    public static partial class Pagination
    {
        public static async Task<IPaginateResult<T>> PaginateAsync<T>(this IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            return await PaginateTask.Paginate(source: source, filter: filter, async: true, options: options);
        }
        public static IPaginateResult<T> Paginate<T>(this IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            return PaginateTask.Paginate(source: source, filter: filter, async: false, options: options).GetAwaiter().GetResult();
        }
        public static async Task<IPaginateResult<T>> PaginateAsync<T>(this IQueryable<T> source, Action<QueryFilter> options) where T : class
        {
            return await PaginateTask.Paginate(source: source, filter: null, async: true, options: options);
        }
        public static IPaginateResult<T> Paginate<T>(this IQueryable<T> source, Action<QueryFilter> options) where T : class
        {
            return PaginateTask.Paginate(source: source, filter: null, async: false, options: options).GetAwaiter().GetResult();
        }
        public static async Task<IPaginateResult<T>> PaginateAsync<T>(this IQueryable<T> source, string filter) where T : class
        {
            return await PaginateTask.Paginate(source: source, filter: filter, async: true, options: null);
        }
        public static IPaginateResult<T> Paginate<T>(this IQueryable<T> source, string filter) where T : class
        {
            return PaginateTask.Paginate(source: source, filter: filter, async: false, options: null).GetAwaiter().GetResult();
        }
        public static async Task<IPaginateResult<T>> PaginateAsync<T>(this IQueryable<T> source) where T : class
        {
            return await PaginateTask.Paginate(source: source, filter: null, async: true, options: null);
        }
        public static IPaginateResult<T> Paginate<T>(this IQueryable<T> source) where T : class
        {
            return PaginateTask.Paginate(source: source, filter: null, async: false, options: null).GetAwaiter().GetResult();
        }        
    }

}