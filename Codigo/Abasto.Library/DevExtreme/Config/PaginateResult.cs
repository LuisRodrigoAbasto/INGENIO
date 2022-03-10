using Abasto.Library.Interfaces;
using System.Collections.Generic;

namespace Abasto.Library.DevExtreme.Config
{
    public class PaginateResult : IPaginateResult
    {
        public PaginateResult() { }
        public List<object> data { get; set; }
        public int? totalCount { get; set; }
        public int? groupCount { get; set; }
        public List<object> summary { get; set; }
    }
    public class PaginateResult<T> : PaginateResult, IPaginateResult<T>
    {
        public PaginateResult() { }
        public new List<T> data { get; set; }
        public List<object> groupData { get; set; }
    }
    public class PaginateGroup
    {
        public object key { get; set; }
        public List<object> items { get; set; }
        public object count { get; set; }
        public List<object> summary { get; set; }
    }
}
