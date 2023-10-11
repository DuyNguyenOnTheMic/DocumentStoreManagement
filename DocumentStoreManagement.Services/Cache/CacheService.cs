using Newtonsoft.Json;
using StackExchange.Redis;

namespace DocumentStoreManagement.Services.Cache
{
    /// <summary>
    /// Redis Cache Service
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Constructor for database interface
        /// </summary>
        /// <param name="database"></param>
        public CacheService(IDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Get cache or set new if not exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration"></param>
        public async Task<IEnumerable<T>> GetOrSetAsync<T>(string key, Task<IEnumerable<T>> func, TimeSpan expiration)
        {
            RedisValue cached = _database.StringGet(key);
            if (!cached.IsNull)
            {
                // Get the cached value
                return JsonConvert.DeserializeObject<IEnumerable<T>>(cached);
            }

            // Otherwise, set new cache value
            IEnumerable<T> result = await func;
            await _database.StringSetAsync(key, JsonConvert.SerializeObject(result), expiration, When.NotExists);
            return result;
        }

        /// <summary>
        /// Flush cache values
        /// </summary>
        /// <param name="key"></param>
        public async Task FlushAsync(string key)
        {
            // Flush cached value by key
            await _database.KeyDeleteAsync(key);
        }
    }
}