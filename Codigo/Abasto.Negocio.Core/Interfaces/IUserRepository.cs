using Abasto.Negocio.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abasto.Negocio.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
    }
}