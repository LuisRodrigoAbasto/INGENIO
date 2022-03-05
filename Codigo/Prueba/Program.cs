using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abasto.Library.DevExtreme;
using Prueba.Model;

namespace Prueba
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using(Context cn=new Context())
            {
                var result = cn.gavAnimal.Take(100).OrderByDescending(x => x.aniId).AsQueryable();
                    var lista=await result.PaginateResultAsync();
                var result2 =  cn.gavAnimal.Take(100).OrderByDescending(x => x.aniId).PaginateResult();
                var obj = result;
            }
        }        
    }
}
