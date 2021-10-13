using Abasto.Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(string id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}
