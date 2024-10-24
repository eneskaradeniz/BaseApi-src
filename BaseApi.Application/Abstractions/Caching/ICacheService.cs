using Microsoft.Extensions.Caching.Distributed;

namespace BaseApi.Application.Abstractions.Caching;

public interface ICacheService
{
    Task<T?> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, DistributedCacheEntryOptions? options = null, CancellationToken cancellationToken = default);

    T? Get<T>(string key);
    
    Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions? options = null, CancellationToken cancellationToken = default);
}