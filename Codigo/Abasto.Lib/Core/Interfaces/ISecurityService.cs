using Abasto.Lib.Core.Entities;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}