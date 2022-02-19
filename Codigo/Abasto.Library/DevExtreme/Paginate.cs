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
    public class Paginate:IPaginate
    {
        public  async Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            return await PaginateTask.Paginate<T>(source: source, filter: filter, async: true, options: options);
        }
        public  IPaginateResult<T> PaginateResult<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class
        {
            return PaginateTask.Paginate<T>(source: source, filter: filter, async: false, options: options).GetAwaiter().GetResult();
        }
        public  async Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source, Action<QueryFilter> options) where T : class
        {
            return await PaginateTask.Paginate<T>(source: source, filter: null, async: true, options: options);
        }
        public  IPaginateResult<T> PaginateResult<T>(IQueryable<T> source, Action<QueryFilter> options) where T : class
        {
            return PaginateTask.Paginate(source: source, filter: null, async: false, options: options).GetAwaiter().GetResult();
        }
        public  async Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source, string filter) where T : class
        {
            return await PaginateTask.Paginate<T>(source: source, filter: filter, async: true, options: null);
        }
        public  IPaginateResult<T> PaginateResult<T>(IQueryable<T> source, string filter) where T : class
        {
            return PaginateTask.Paginate<T>(source: source, filter: filter, async: false, options: null).GetAwaiter().GetResult();
        }
        public  async Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source) where T : class
        {
            return await PaginateTask.Paginate<T>(source: source, filter: null, async: true, options: null);
        }
        public  IPaginateResult<T> PaginateResult<T>(IQueryable<T> source) where T : class
        {
            return PaginateTask.Paginate<T>(source: source, filter: null, async: false, options: null).GetAwaiter().GetResult();
        }        
    }

}