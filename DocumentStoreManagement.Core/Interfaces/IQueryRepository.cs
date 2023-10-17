namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Query Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
    }
}
