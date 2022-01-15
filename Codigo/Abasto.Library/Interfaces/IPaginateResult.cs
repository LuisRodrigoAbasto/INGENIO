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
        public List<T> data { get; set; }
        public List<object> groupData { get; set; }
        public int? totalCount { get; set; }
        public int? groupCount { get; set; }
        public List<object> summary { get; set; }
    }
}
