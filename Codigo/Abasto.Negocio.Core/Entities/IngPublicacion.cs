using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Abasto.Negocio.Core.Entities
{
    public partial class IngPublicacion
    {
        public IngPublicacion()
        {
            IngComentario = new HashSet<IngComentario>();
        }
        [Key]
        public long PubId { get; set; }
        public long UsuId { get; set; }

        [Column(TypeName = "date")]
        public DateTime PubFecha { get; set; }

        [Required, StringLength(1000), Column(TypeName = "varchar")]
        public string PubDescripcion { get; set; }
        public string PubImagen { get; set; }

        [ForeignKey("UsuId")]
        public virtual IngUsuario Usu { get; set; }

        public virtual ICollection<IngComentario> IngComentario { get; set; }
    }
}
