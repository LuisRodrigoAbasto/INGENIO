using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Negocio
{
    [Table("parProducto")]
    public partial class parProducto
    {
        [Key]
        public long proId { get; set; }
        public long empId { get; set; }
        
        [ForeignKey("empId")]
        public virtual parEmpresa parEmpresa { get; set; }
        public long catId { get; set; }

        [ForeignKey("catId")]
        public virtual parCategoria parCategoria { get; set; }
    }
}