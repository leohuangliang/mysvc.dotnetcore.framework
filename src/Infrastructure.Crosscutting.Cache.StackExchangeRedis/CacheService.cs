using Microsoft.Extensions.Caching.Distributed;
using MySvc.Framework.Infrastructure.Crosscutting.Cache;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.Framework.Infrastructure.Crosscutting.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Crosscutting.StackExchangeRedis
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IJsonConverter _jsonConverter;

        public CacheService(IDistributedCache distributedCache, IJsonConverter jsonConverter)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _jsonConverter = jsonConverter ?? throw new ArgumentNullException(nameof(jsonConverter));
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            var result = await _distributedCache.GetStringAsync(key, token);
            if (result.IsNullOrBlank()) return default(T);

            return _jsonConverter.DeserializeObject<T>(result);
        }

        public async Task SetAsync<T>(string key, T data, CancellationToken token = default)
        {
            string value = string.Empty;
            if (data != null)
            {
                value = _jsonConverter.SerializeObject(data);
            }

            await _distributedCache.SetStringAsync(key, value, token);
        }

        public async Task SetAsync<T>(string key, T data, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            string value = string.Empty;
            if (data != null)
            {
                value = _jsonConverter.SerializeObject(data);
            }

            await _distributedCache.SetStringAsync(key, value, options, token);
        }
    }
}
