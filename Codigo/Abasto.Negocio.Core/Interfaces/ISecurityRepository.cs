using Abasto.Negocio.Core.Entities;
using System.Threading.Tasks;

namespace Abasto.Negocio.Core.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}