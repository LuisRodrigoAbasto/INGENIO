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
    public class ContratoRepository : BaseRepository<Contrato>, IContratoRepository
    {
        public ContratoRepository(NegocioContext context) : base(context) { }        
    }
}
