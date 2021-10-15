using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> Create(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<int> Update(T entity);
        Task<int> Delete(int id);
    }
}
