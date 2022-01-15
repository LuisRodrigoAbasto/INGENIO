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
        Task<IPaginateResult<T>> Paginate<T>(IQueryable<T> source, string filter, bool async, Action<QueryFilter> options) where T : class;

    }
}
