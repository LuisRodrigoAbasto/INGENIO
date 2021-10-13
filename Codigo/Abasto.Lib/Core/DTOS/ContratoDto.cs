using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.DTOS
{
    public partial class ContratoDto
    {
        public string CodigoContrato { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Testimonio { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaTextimonio { get; set; }
        public int NumeroNotaria { get; set; }
        public string PaternoProveedor { get; set; }
        public string MaternoProveedor { get; set; }
        public string NombresProveedor { get; set; }
        public string DocumentoProveedor { get; set; }
        public string Domicilio { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int Superficie { get; set; }
        public string NumeroDireccion { get; set; }
        public string Importe { get; set; }
        public string Literal { get; set; }
        public string Cuenta { get; set; }
        public string NumeroMeses { get; set; }
        public DateTime FechaInicialArrendamiento { get; set; }
        public DateTime FechaFinalArrendamiento { get; set; }
        public DateTime FechaTenor { get; set; }
        public DateTime Mes { get; set; }
        public DateTime Anio { get; set; }
    }
}
