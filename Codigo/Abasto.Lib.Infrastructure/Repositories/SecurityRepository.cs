using Microsoft.EntityFrameworkCore;
using Abasto.Lib.Core.Entities;
using Abasto.Lib.Core.Interfaces;
using Abasto.Lib.Infrastructure.Data;
using System.Threading.Tasks;

namespace Abasto.Lib.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(NegocioContext context) : base(context) { }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == login.User);
        }
    }
}
