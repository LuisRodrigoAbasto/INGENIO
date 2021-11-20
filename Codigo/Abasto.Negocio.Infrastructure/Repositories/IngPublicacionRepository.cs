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
        public async Task<IEnumerable<ingPublicacion>> ToListAsync()
        {
            var lista = await this._context.ingPublicacion.ToListAsync();
            return lista;
        }

        public async Task<ingPublicacion> FirstOrDefaultAsync(long id)
        {
            var obj = await this._context.ingPublicacion.Where(x=>x.pubId==id).FirstOrDefaultAsync();
            return obj;
        }
        public async Task Add(ingPublicacion obj)
        {
            this._context.ingPublicacion.Add(obj);
            await this._context.SaveChangesAsync();
        }
        public async Task Update(ingPublicacion obj)
        {
            this._context.ingPublicacion.Update(obj);
            await this._context.SaveChangesAsync();
        }
        public async Task Delete(long id)
        {
            var obj = await FirstOrDefaultAsync(id);
            this._context.ingPublicacion.Remove(obj);
            await this._context.SaveChangesAsync();
        }
    }
}
