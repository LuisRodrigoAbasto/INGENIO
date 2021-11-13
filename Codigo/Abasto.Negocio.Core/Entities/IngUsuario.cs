using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Abasto.Negocio.Core.Entities
{
    [Table("ingUsuario")]
    public partial class ingUsuario
    {
        public ingUsuario()
        {
            ingComentario = new HashSet<ingComentario>();
            ingPublicacion = new HashSet<ingPublicacion>();
        }
        [Key]
        public long usrId { get; set; }
        
        [Required,StringLength(100),Column(TypeName ="varchar")]
        public string usrNombre { get; set; }
        
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string usrEmail { get; set; }
        
        [Column(TypeName ="date")]
        public DateTime usrFechaNacimiento { get; set; }
        
        [StringLength(10), Column(TypeName = "varchar")]
        public string usrTelefono { get; set; }
        
        public bool usrActivo { get; set; }

        public virtual ICollection<ingComentario> ingComentario { get; set; }
        public virtual ICollection<ingPublicacion> ingPublicacion { get; set; }
    }
}
