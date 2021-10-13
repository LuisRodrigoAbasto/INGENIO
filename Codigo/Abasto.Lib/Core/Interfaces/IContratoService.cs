using Abasto.Lib.Core.CustomEntities;
using Abasto.Lib.Core.Entities;
using Abasto.Lib.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface IContratoService
    {
        PagedList<Contrato> GetContrato(PostQueryFilter filters);

        Task<Contrato> GetContrato(string id);

        Task InsertContrato(Contrato post);

        Task<bool> UpdateContrato(Contrato post);

        Task<bool> DeleteContrato(string id);
    }
}