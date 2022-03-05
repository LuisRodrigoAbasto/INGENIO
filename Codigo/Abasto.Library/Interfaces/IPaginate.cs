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
    public interface IPaginate<T>
    {
        Task<IPaginateResult<T>> PaginateResultAsync<T>();
        IPaginateResult<T> PaginateResult<T>();
    }
    public interface IPaginateQuery<T>:IPaginate<T>
    {
        IQueryable<T> Query { get;}
        string Filter { get; }
        Action<QueryFilter> Options { get;}
    }
    public class PaginateQuery<T>: Paginate<T>, IPaginateQuery<T>
    {
        private IQueryable<T> _source;
        private string _filter;
        private Action<QueryFilter> _options;

        public PaginateQuery(IQueryable<T> source, string filter, Action<QueryFilter> options) : base(source, filter, options)
        {
            this._source = source;
            this._filter = filter;
            this._options = options;
        }

        public IQueryable<T> Query { get { return _source; } }
        public string Filter { get { return _filter; } }
        public Action<QueryFilter> Options { get { return _options; } }
    }
}
