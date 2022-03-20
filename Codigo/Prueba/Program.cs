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
            Console.WriteLine("Inicio");
            using (Context cn = new Context())
            {
                try
                {
                    long result = cn.gavAnimal.LongCount();
                    var result4 = cn.gavAnimal.PaginateResult();

                    Console.WriteLine($"Cantidad de Animales {result}");
                    //Console.WriteLine(JsonConvert.SerializeObject(result));
                    int result2 = cn.gavAnimal.GroupBy(x => x.catId).Count();
                    //Console.WriteLine(JsonConvert.SerializeObject(result2));
                    Console.WriteLine("Cantidad de Categoria en Animales=>" + result2);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Cantidad de Categoria en Animales=>" + ex.Message);
                }
            }
            Console.WriteLine("Fin");
        }
    }
}
