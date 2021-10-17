using Abasto.Negocio.Core.CustomEntities;
using Abasto.Negocio.Core.Entities;
using Abasto.Negocio.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abasto.Negocio.Core.Interfaces
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