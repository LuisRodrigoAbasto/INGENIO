using Abasto.Negocio.Core.Entities;
using Abasto.Negocio.Core.Interfaces;
using Abasto.Negocio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Negocio.Infrastructure.Repositories
{
    public class IngPublicacionRepository:IIngPublicacionRepository
    {
        private readonly NegocioContext _context;
        public IngPublicacionRepository(NegocioContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<IngPublicacion>> ToListAsync()
        {
            var lista = await this._context.IngPublicacion.ToListAsync();
            return lista;
        }

        public async Task<IngPublicacion> Get(long id)
        {
            var obj = await this._context.IngPublicacion.Where(x=>x.PubId==id).FirstOrDefaultAsync();
            return obj;
        }
        public async Task Add(IngPublicacion obj)
        {
            this._context.IngPublicacion.Add(obj);
            await this._context.SaveChangesAsync();
        }
    }
}
