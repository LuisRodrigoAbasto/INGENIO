using Abasto.Lib.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IContratoRepository ContratoRepository { get; }

        IRepository<Contrato> ContratoRepository { get; }

        //IRepository<Comment> CommentRepository { get; }

        ISecurityRepository SecurityRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
