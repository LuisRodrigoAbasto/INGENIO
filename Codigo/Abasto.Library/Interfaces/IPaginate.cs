using Abasto.Library.DevExtreme;
using Abasto.Library.DevExtreme.Config;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Abasto.Library.Interfaces
{
    public interface IPaginateQueryProcess
    {
        Task<IPaginateQuery> PaginateResultAsync();
        IPaginateQuery PaginateResult();
    }
    public interface IPaginateResultProcess<out T>
    {
        new Task<IPaginateResult<T>> PaginateResultAsync<T>();
        new IPaginateResult<T> PaginateResult<T>();
    }
    public interface IPaginateQuery<out T>
    {
    }
    public class PageResultProcess<T> : PaginateResultProcess<T>, IPaginateQuery<T>
    {
        public PageResultProcess(IQueryable<T> source, string filter, Action<QueryFilter> options) : base(source, filter, options)
        {
        }
    }
}
