namespace Abasto.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seguridad")]
    public partial class Seguridad
    {
        [Key]
        public int IdSeguridad { get; set; }

        [StringLength(50)]
        public string Usuario { get; set; }

        [StringLength(100)]
        public string NombreUsuario { get; set; }

        [StringLength(200)]
        public string Contrasena { get; set; }

        [StringLength(15)]
        public string Rol { get; set; }
    }
}
