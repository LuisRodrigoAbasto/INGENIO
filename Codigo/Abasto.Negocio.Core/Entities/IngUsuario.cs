using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Abasto.Negocio.Core.Entities
{
    [Table("IngUsuario")]
    public partial class IngUsuario
    {
        public IngUsuario()
        {
            IngComentario = new HashSet<IngComentario>();
            IngPublicacion = new HashSet<IngPublicacion>();
        }
        [Key]
        public long UsuId { get; set; }
        
        [Required,StringLength(100),Column(TypeName ="varchar")]
        public string UsuNombre { get; set; }
        
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string UsuEmail { get; set; }
        
        [Column(TypeName ="date")]
        public DateTime UsuFechaNacimiento { get; set; }
        
        [StringLength(10), Column(TypeName = "varchar")]
        public string UsuTelefono { get; set; }
        
        public bool UsuActivo { get; set; }

        public virtual ICollection<IngComentario> IngComentario { get; set; }
        public virtual ICollection<IngPublicacion> IngPublicacion { get; set; }
    }
}
