using System;
using System.Collections.Generic;

#nullable disable

namespace Abasto.Negocio.Infrastructure.Data
{
    public partial class Comentario
    {
        public long IdComentario { get; set; }
        public long IdPublicacion { get; set; }
        public long IdUsuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }

        public virtual Publicacion IdPublicacionNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
