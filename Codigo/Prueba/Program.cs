using Abasto.Library.DevExtreme;
using Newtonsoft.Json;
using Prueba.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Prueba
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (Context cn = new Context())
            {
                var result = cn.gavAnimal.Take(100).OrderByDescending(x => x.aniId).AsQueryable();
                var lista = await result.PaginateResultAsync();
                var ok = lista.data;
                var result2 = cn.gavAnimal.Take(100).OrderByDescending(x => x.aniId).PaginateResult();
                var obj = result;
                Console.WriteLine(JsonConvert.SerializeObject(lista));
            }
        }
    }
}
