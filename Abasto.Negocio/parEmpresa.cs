using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abasto.Negocio
{
    [Table("parEmpresa")]
    public partial class parEmpresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parEmpresa()
        {
            parProducto = new HashSet<parProducto>();
            parCategoria = new HashSet<parCategoria>();
            parVentaTxn = new HashSet<parVentaTxn>();
        }
        [Key]
        public long empId { get; set; }

        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string empNombre { get; set; }
        
        [StringLength(200), Column(TypeName = "varchar")]
        public string empDireccion { get; set; }
        
        [StringLength(30), Column(TypeName = "varchar")]
        public string empTelefono { get; set; }

        [Required, StringLength(1), Column(TypeName = "char")]
        public string empEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parProducto> parProducto { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parCategoria> parCategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parVentaTxn> parVentaTxn { get; set; }
    }
}