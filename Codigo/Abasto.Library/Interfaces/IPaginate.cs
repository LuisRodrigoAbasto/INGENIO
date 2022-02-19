using Abasto.Library.DevExtreme.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Library.Interfaces
{
    public interface IPaginate
    {
        Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class;
        IPaginateResult<T> PaginateResult<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class;
        Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source, Action<QueryFilter> options) where T : class;
        IPaginateResult<T> PaginateResult<T>(IQueryable<T> source, Action<QueryFilter> options) where T : class;
        Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source, string filter) where T : class;
        IPaginateResult<T> PaginateResult<T>(IQueryable<T> source, string filter) where T : class;
        Task<IPaginateResult<T>> PaginateResultAsync<T>(IQueryable<T> source) where T : class;
        IPaginateResult<T> PaginateResult<T>(IQueryable<T> source) where T : class;
    }
}
