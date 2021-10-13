using Abasto.Lib.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
    }
}