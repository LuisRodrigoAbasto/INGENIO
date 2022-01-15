using System;
using System.Collections.Generic;
using System.Linq;
using Abasto.Library.DevExtreme;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lista=new List<string>() { "ok","mam"};
            var result = lista.AsQueryable().Paginate();           
            foreach (var item in result.data) {
                Console.WriteLine(item);
            }
        }
    }
}
