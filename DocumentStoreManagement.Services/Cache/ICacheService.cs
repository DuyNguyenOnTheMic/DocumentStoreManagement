namespace DocumentStoreManagement.Services.Cache
{
    /// <summary>
    /// Interface for cache service
    /// </summary>
    public interface ICacheService
    {
        /// <inheritdoc/>
        Task<IEnumerable<T>> GetOrSetAsync<T>(string key, Func<Task<IEnumerable<T>>> func, TimeSpan expiration);

        /// <inheritdoc/>
        Task FlushAsync(string key);
    }
}
