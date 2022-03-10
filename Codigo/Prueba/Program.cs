using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abasto.Library.DevExtreme;
using Prueba.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                var ok=lista.data;
                var result2 =  cn.gavAnimal.Take(100).OrderByDescending(x => x.aniId).PaginateResult();
                var obj = result;
                Console.WriteLine(JsonConvert.SerializeObject(lista));
            }
        }        
    }
}
