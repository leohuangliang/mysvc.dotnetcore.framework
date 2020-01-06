using System.Collections.Generic;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Cache
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Gets the value associated with the specified keys.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="keys">The key of the value to get.</param>
        /// <returns>The values associated with the specified keys.</returns>
        T[] Get<T>(string[] keys);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTimeInMinute">Cache time</param>
        void Set(string key, object data, int cacheTimeInMinute);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        void Set(string key, object data);

        /// <summary>
        /// Adds multiple key and object to the cache.
        /// </summary>
        /// <param name="keyValues"></param>
        void Set(IDictionary<string, object> keyValues);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// Clear all cache data
        /// </summary>
        void Clear();
    }
}