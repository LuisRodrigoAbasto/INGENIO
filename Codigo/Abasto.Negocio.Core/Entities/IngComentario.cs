using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Abasto.Negocio.Core.Entities
{
    [Table("ingComentario")]
    public partial class ingComentario
    {
        [Key]
        public long comId { get; set; }
        public long pubId { get; set; }
        public long usrId { get; set; }
        
        [Required, StringLength(500), Column(TypeName = "varchar")]
        public string comDescripcion { get; set; }
        
        [Column(TypeName = "datetime")]
        public DateTime comFecha { get; set; }
        public bool comActivo { get; set; }

        [ForeignKey("usrId")]
        public virtual ingUsuario ingUsuario { get; set; }

        [ForeignKey("pubId")]
        public virtual ingPublicacion ingPublicacion { get; set; }
    }
}
