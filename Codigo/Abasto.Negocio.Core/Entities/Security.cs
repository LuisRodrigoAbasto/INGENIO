using Abasto.Negocio.Core.Enumerations;

namespace Abasto.Negocio.Core.Entities
{
    public class Security
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public RoleType Role { get; set; }
    }
}
