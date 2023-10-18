using System.Linq.Expressions;

namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> FindAsync(object expression);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression);
    }
}
