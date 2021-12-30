using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Cache
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key, CancellationToken token = default(CancellationToken));

        Task SetAsync<T>(string key, T data, CancellationToken token = default(CancellationToken));

        Task SetAsync<T>(string key, T data, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken));
    }
}
