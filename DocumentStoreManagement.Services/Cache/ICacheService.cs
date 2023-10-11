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
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetOrSetAsync<T>(string key, Task<IEnumerable<T>> func, TimeSpan expiration);

        /// <summary>
        /// Flush cache values
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task FlushAsync(string key);
    }
}
