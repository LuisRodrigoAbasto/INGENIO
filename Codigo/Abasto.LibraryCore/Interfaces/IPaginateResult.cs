using Abasto.Library.DevExtreme.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Library.Interfaces
{
    public interface IPaginateResult<T>
    {
        List<T> data { get; set; }
        List<object> groupData { get; set; }
        int? totalCount { get; set; }
        int? groupCount { get; set; }
        List<object> summary { get; set; }
    }
    public interface IPaginateResult
    {
        List<object> data { get; set; }
        int? totalCount { get; set; }
        int? groupCount { get; set; }
        List<object> summary { get; set; }
    }
}
