using Abasto.Negocio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Negocio.Core.Interfaces
{
    public interface IIngPublicacionRepository
    {
        Task<IEnumerable<IngPublicacion>> ToListAsync();
        Task<IngPublicacion> Get(long id);
        Task Add(IngPublicacion obj);
    }
}
