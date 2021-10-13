using Microsoft.EntityFrameworkCore;
using Abasto.Lib.Core.Entities;
using Abasto.Lib.Core.Interfaces;
using Abasto.Lib.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Lib.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(NegocioContext context) : base(context) { }

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
