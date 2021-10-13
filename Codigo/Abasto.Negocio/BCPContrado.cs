namespace Abasto.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BCPContrado")]
    public partial class BCPContrado
    {
        [Key]
        [StringLength(10)]
        public string CodigoContrato { get; set; }

        [StringLength(30)]
        public string Paterno { get; set; }

        [StringLength(30)]
        public string Materno { get; set; }

        [StringLength(30)]
        public string Nombres { get; set; }

        [StringLength(50)]
        public string Testimonio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaInicial { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinal { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaTestimonio { get; set; }

        public int? NumeroNotaria { get; set; }

        [StringLength(30)]
        public string PaternoProveedor { get; set; }

        [StringLength(30)]
        public string MaternoProveedor { get; set; }

        [StringLength(30)]
        public string NombresProveedor { get; set; }

        [StringLength(20)]
        public string DocumentoProveedor { get; set; }

        [StringLength(500)]
        public string Domicilio { get; set; }

        [StringLength(500)]
        public string Direccion { get; set; }

        [StringLength(30)]
        public string Ciudad { get; set; }

        public int? Superficie { get; set; }

        [StringLength(10)]
        public string NumeroDireccion { get; set; }

        public decimal? Importe { get; set; }

        [StringLength(200)]
        public string Literal { get; set; }

        [StringLength(20)]
        public string Cuenta { get; set; }

        public int? NumeroMeses { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaInicialArrendamiento { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinalArrendamiento { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaTenor { get; set; }

        [StringLength(30)]
        public string Mes { get; set; }

        [StringLength(4)]
        public string Anio { get; set; }
    }
}
