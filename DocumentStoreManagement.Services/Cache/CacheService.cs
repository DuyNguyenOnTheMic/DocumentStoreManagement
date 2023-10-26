using Newtonsoft.Json;
using StackExchange.Redis;

namespace DocumentStoreManagement.Services.Cache
{
    /// <summary>
    /// Redis Cache Service
    /// </summary>
    /// <remarks>
    /// Constructor for database interface
    /// </remarks>
    /// <param name="database"></param>
    public class CacheService(IDatabase database) : ICacheService
    {
        private readonly IDatabase _database = database;

        /// <summary>
        /// Get cache or set new if not exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration"></param>
        public async Task<IEnumerable<T>> GetOrSetAsync<T>(string key, Func<Task<IEnumerable<T>>> func, TimeSpan expiration)
        {
            RedisValue cached = await _database.StringGetAsync(key);
            if (!cached.IsNull)
            {
                // Get the cached value
                using StringReader sr = new(cached);
                using JsonTextReader jr = new(sr);
                JsonSerializer serializer = new();
                return serializer.Deserialize<IEnumerable<T>>(jr);
            }

            // Otherwise, set new cache value
            IEnumerable<T> result = await func();
            using (StringWriter sw = new())
            {
                using JsonTextWriter jw = new(sw);
                JsonSerializer serializer = new();
                serializer.Serialize(jw, result);
                await _database.StringSetAsync(key, sw.ToString(), expiration, When.NotExists);
            }
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