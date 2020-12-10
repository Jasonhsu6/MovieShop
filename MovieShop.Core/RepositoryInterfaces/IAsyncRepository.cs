using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    // T: class, reference type
    // T: struct, value type
    public interface IAsyncRepository<T> where T: class
    {
        //CRUD operations which are common across all the repositories
        //Get an entity by id => movieid => Movie
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> ListAllWithIncludesAsync(Expression<Func<T, bool>> @where, params Expression<Func<T, object>>[] includes);
    }
}
