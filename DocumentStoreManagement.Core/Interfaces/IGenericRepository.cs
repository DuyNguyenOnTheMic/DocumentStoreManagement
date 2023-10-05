using System.Linq.Expressions;

namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression);
    }
}
