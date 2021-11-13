using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Abasto.Negocio.Core.Entities
{
    [Table("ingPublicacion")]
    public partial class ingPublicacion
    {
        public ingPublicacion()
        {
            ingComentario = new HashSet<ingComentario>();
        }
        [Key]
        public long pubId { get; set; }
        public long usrId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime pubFecha { get; set; }

        [Required, StringLength(1000), Column(TypeName = "varchar")]
        public string pubDescripcion { get; set; }

        [StringLength(500), Column(TypeName = "varchar")]
        public string pubImagen { get; set; }

        [ForeignKey("usrId")]
        public virtual ingUsuario ingUsuario { get; set; }

        public virtual ICollection<ingComentario> ingComentario { get; set; }
    }
}
