using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Negocio
{
    [Table("parVentaLogTxn")]
    public partial class parVentaLogTxn
    {
        [Key]
        public long logId { get; set; }

        public long venId { get; set; }
        public long usrId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime logFecha { get; set; }

        [Required, StringLength(1), Column(TypeName = "char")]
        public string logEstado { get; set; }

        [StringLength(200), Column(TypeName = "varchar")]
        public string logComentario { get; set; }

        [ForeignKey("venId")]
        public virtual parVentaTxn parVentaTxn { get; set; }
    }
}
