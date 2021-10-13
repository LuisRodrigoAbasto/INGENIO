using Abasto.Lib.Core.Entities;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}