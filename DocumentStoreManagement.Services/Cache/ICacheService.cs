namespace DocumentStoreManagement.Services.Cache
{
    /// <summary>
    /// Interface for cache service
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Get cache or set new if not exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration"></param>
        Task<IEnumerable<T>> GetOrSetAsync<T>(string key, Func<Task<IEnumerable<T>>> func, TimeSpan expiration);

        /// <summary>
        /// Flush cache values
        /// </summary>
        /// <param name="key"></param>
        /// <inheritdoc/>
        Task FlushAsync(string key);
    }
}
