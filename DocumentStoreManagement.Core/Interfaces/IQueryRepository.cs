namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Query Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get(string query);
        Task<IEnumerable<T>> GetAllAsync(string table);
        Task<IEnumerable<T>> GetAllWithIncludeAsync(string table, string includeTable);
        Task<IEnumerable<T>> GetBetweenDatesAsync(string table, string column, string from, string to);
        Task<T> GetByIdAsync(string table, object id);
    }
}
