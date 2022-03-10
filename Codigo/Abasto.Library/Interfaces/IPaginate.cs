using Abasto.Library.DevExtreme;
using Abasto.Library.DevExtreme.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Library.Interfaces
{
    public interface IPaginate<out T>
    {
        Task<IPaginateResult<T>> PaginateResultAsync<T>();
        IPaginateResult<T> PaginateResult<T>();
    }
    public interface IPaginateQuery<out T>
    {
    }
    public class PaginateQuery<T> : Paginate<T>,IPaginateQuery<T>
    {
        public PaginateQuery(IQueryable<T> source, string filter, Action<QueryFilter> options) : base(source, filter, options)
        {
        }      
    }
}
