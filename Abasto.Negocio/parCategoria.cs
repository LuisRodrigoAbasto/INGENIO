using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abasto.Negocio
{
    [Table("parCategoria")]
    public partial class parCategoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parCategoria()
        {
            parProducto = new HashSet<parProducto>();
        }
        [Key]
        public long catId { get; set; }
        public long empId { get; set; }

        [ForeignKey("empId")]
        public virtual parEmpresa parEmpresa { get; set; }

        [Required,StringLength(50),Column(TypeName ="varchar")]
        public string catNombre { get; set; }

        [Required, StringLength(1), Column(TypeName = "char")]
        public string catEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parProducto> parProducto { get; set; }
    }
}