using System.Linq.Expressions;

namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Query Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string table);
        Task<IQueryable<T>> GetQueryWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(string table, object id);
    }
}
