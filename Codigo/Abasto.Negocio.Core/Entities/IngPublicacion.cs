using System;

namespace Abasto.Negocio.Core.Entities
{
    public class IngPublicacion
    {
        public long PubId { get; set; }
        public long UsuId { get; set; }
        public DateTime PubFecha { get; set; }
        public string PubDescripcion { get; set; }
        public string PubImage { get; set; }
    }
}
