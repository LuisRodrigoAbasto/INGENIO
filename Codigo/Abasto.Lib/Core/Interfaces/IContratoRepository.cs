using Abasto.Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface IContratoRepository : IRepository<Contrato>
    {
        Task<IEnumerable<Contrato>> GetContratoByUser(int userId);
    }
}
