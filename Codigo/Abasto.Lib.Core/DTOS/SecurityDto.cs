using Abasto.Lib.Core.Enumerations;

namespace Abasto.Lib.Core.DTOS
{
    public class SecurityDto
    {
        public string User { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public RoleType? Role { get; set; }
    }
}
