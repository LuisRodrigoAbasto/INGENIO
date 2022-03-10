using System.Collections.Generic;

namespace Abasto.Library.Interfaces
{
    public interface IPaginateResult
    {
        List<object> data { get; set; }
        int? totalCount { get; set; }
        int? groupCount { get; set; }
        List<object> summary { get; set; }
    }
    public interface IPaginateResult<T> : IPaginateResult
    {
        new List<T> data { get; set; }
        List<object> groupData { get; set; }
    }
}
