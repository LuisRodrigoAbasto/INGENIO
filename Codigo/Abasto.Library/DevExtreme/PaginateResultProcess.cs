using Abasto.Library.DevExtreme.Config;
using Abasto.Library.Interfaces;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Abasto.Library.DevExtreme
{
    public class PaginateResultProcess<T> :PaginateQueryProcess, IPaginateResultProcess<T>
    {
        public PaginateResultProcess(IQueryable<T> source,string filter,Action<QueryFilter> option) : base(source, filter, option)
        {           
        }

        public async Task<IPaginateResult<T>> PaginateResultAsync<T>()
        {
            return await PaginateExecute<T>(true);
        }
        public IPaginateResult<T> PaginateResult<T>()
        {
            return PaginateExecute<T>(false).GetAwaiter().GetResult();
        }
        private async Task<IPaginateResult<T>> PaginateExecute<T>(bool async)
        {
            var baseResult = async?await this.PaginateResultAsync():PaginateResult();
            PaginateResult<T> result = new PaginateResult<T>()
            {
                totalCount=baseResult.totalCount,
                groupCount=baseResult.groupCount,
                summary=baseResult.summary,
            };
            if (this.isGroup) result.groupData = baseResult.data.ToDynamicList<object>();
            else result.data = baseResult.data.ToDynamicList<T>();

            return result;
        }
    }
}
