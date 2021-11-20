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
        Task<IEnumerable<ingPublicacion>> ToListAsync();
        Task<ingPublicacion> FirstOrDefaultAsync(long id);
        Task Add(ingPublicacion obj);
        Task Update(ingPublicacion obj);
        Task Delete(long id);
    }
}
