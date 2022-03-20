using System.Collections.Generic;
using System.Linq;

namespace Abasto.Library.Interfaces
{
    public interface IPaginateQuery
    {
        IQueryable data { get; set; }
        int? totalCount { get; set; }
        int? groupCount { get; set; }
        List<object> summary { get; set; }
    }
    public interface IPaginateResult:IPaginateQuery
    {
        new List<object> data { get; set; }
    }
    public interface IPaginateResult<T> : IPaginateQuery
    {
        new List<T> data { get; set; }
        List<object> groupData { get; set; }
    }
}
