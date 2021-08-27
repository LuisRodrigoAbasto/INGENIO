using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Negocio
{
    [Table("parVentaTxn")]
    public partial class parVentaTxn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parVentaTxn()
        {
            parVentaLogTxn = new HashSet<parVentaLogTxn>();
        }
        [Key]
        public long venId { get; set; }
        public long empId { get; set; }

        [ForeignKey("empId")]
        public virtual parEmpresa parEmpresa { get; set; }
        public long cliId { get; set; }
        
        [Column(TypeName = "datetime")]
        public DateTime venFecha { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime venFechaCreacion { get; set; }

        [Required, StringLength(1), Column(TypeName = "char")]
        public string venEstado { get; set; }

        [StringLength(200), Column(TypeName = "varchar")]
        public string venComentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parVentaLogTxn> parVentaLogTxn { get; set; }
    }
}