using System.Linq.Expressions;

namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Nothing</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Add range of items
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Nothing</returns>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>A list of items</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get all items with include
        /// </summary>
        /// <param name="includes"></param>
        /// <returns>A list of items with child table included</returns>
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Find item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An item matches the id</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Query list by expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>A list of items matches the expression</returns>
        Task<IEnumerable<T>> FindAsync(object expression);

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Nothing</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Remove an item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Nothing</returns>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Remove range of items
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Nothing</returns>
        Task RemoveRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Check if an expression exists
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>Boolean</returns>
        Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression);
    }
}
