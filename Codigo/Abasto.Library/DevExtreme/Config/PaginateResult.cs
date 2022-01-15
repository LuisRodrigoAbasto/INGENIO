using Abasto.Library.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Abasto.Library.DevExtreme.Config
{
    public class PaginateResult<T>:IPaginateResult<T>
    {
        public List<T> data { get; set; }
        public List<object> groupData { get; set; }
        public int? totalCount { get; set; }
        public int? groupCount { get; set; }
        public List<object>? summary { get; set; }
    }
    public class PaginateGroup
    {
        public object key { get; set; }
        public List<object> items { get; set; }
        public object? count { get; set; }
        public List<object> summary { get; set; }
    }
}
