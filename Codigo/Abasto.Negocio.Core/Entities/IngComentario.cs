using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Abasto.Negocio.Core.Entities
{
    [Table("IngComentario")]
    public partial class IngComentario
    {
        [Key]
        public long ComId { get; set; }
        public long PubId { get; set; }
        public long UsuId { get; set; }
        
        [Required, StringLength(500), Column(TypeName = "varchar")]
        public string ComDescripcion { get; set; }
        
        [Column(TypeName = "datetime")]
        public DateTime ComFecha { get; set; }
        public bool ComActivo { get; set; }

        [ForeignKey("UsuId")]
        public virtual IngUsuario Usu { get; set; }

        [ForeignKey("PubId")]
        public virtual IngPublicacion Pub { get; set; }
    }
}
