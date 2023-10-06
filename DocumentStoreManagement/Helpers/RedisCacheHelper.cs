using Newtonsoft.Json;
using StackExchange.Redis;

namespace DocumentStoreManagement.Helpers
{
    /// <summary>
    /// Redis Cache Helper
    /// </summary>
    /// <param name="Database"></param>
    public record RedisCacheHelper(IDatabase Database)
    {
        /// <summary>
        /// Get cache or set new if not exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetOrSetAsync<T>(string key, Func<Task<IEnumerable<T>>> func)
        {
            RedisValue cached = await Database.StringGetAsync(key);
            if (cached.HasValue)
            {
                // Get the cached value
                return JsonConvert.DeserializeObject<IEnumerable<T>>(cached);
            }

            // Otherwise, set new cache value
            IEnumerable<T> result = await func();
            await Database.StringSetAsync(key, JsonConvert.SerializeObject(result));
            return result;
        }
    }
}
