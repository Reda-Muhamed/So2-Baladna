using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Interfaces
{// implementing a generic repository interface for CRUD operations in  repositories
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id) ;
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyList<T>> GetAllAsync() ;
        Task<IReadOnlyList<T>> GetAllAsync(params Expression< Func<T, object>>[]includes);
        Task AddAsync(T entity) ;
        Task UpdateAsync(T entity) ;
        Task DeleteAsync(int id) ;
    }
}
