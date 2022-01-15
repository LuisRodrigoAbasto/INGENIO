using Abasto.Library.DevExtreme.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Library.Interfaces
{
    public interface IPaginateRepository
    {
        Task<PaginateResult<T>> PaginateAsync<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class;
        PaginateResult<T> Paginate<T>(IQueryable<T> source, string filter, Action<QueryFilter> options) where T : class;
        Task<PaginateResult<T>> PaginateAsync<T>(IQueryable<T> source, Action<QueryFilter> options) where T : class;
        PaginateResult<T> Paginate<T>(IQueryable<T> source, Action<QueryFilter> options) where T : class;
        Task<PaginateResult<T>> PaginateAsync<T>(IQueryable<T> source, string filter) where T : class;
        PaginateResult<T> Paginate<T>(IQueryable<T> source, string filter) where T : class;
        Task<PaginateResult<T>> PaginateAsync<T>(IQueryable<T> source) where T : class;
        PaginateResult<T> Paginate<T>(IQueryable<T> source) where T : class;        
    }
}
