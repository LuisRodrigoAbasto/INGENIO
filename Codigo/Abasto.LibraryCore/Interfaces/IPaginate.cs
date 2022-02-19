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
        Task<IPaginateResult<T>> PaginateResultAsync<T>() where T : class;
        IPaginateResult<T> PaginateResult<T>() where T : class;
    }
}
