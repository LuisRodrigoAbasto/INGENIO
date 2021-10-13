using Abasto.Lib.Core.CustomEntities;
using Abasto.Lib.Core.Entities;
using Abasto.Lib.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface IPostService
    {
        PagedList<Post> GetPosts(PostQueryFilter filters);

        Task<Post> GetPost(int id);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(int id);
    }
}