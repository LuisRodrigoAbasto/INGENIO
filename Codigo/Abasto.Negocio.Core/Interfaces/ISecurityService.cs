using Abasto.Negocio.Core.Entities;
using System.Threading.Tasks;

namespace Abasto.Negocio.Core.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}