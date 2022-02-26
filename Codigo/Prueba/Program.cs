using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abasto.Library.DevExtreme;

namespace Prueba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lista = new List<Cliente>()
            {
                new Cliente()
                {
                    id=0,
                    nombre="Luis"
                },
                new Cliente()
                {
                    id=2,
                    nombre="Luis"
                },
                new Cliente()
                {
                    id=3,
                    nombre="Luis"
                },
            };
            var result = lista.AsQueryable().PaginateResult();
            var obj = result;
        }
        public class Cliente
        {
            public int id { get; set; }
            public string nombre { get; set; }
        }
    }
}
