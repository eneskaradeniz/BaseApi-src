using BaseApi.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BaseApi.Infrastructure.Caching;

internal class CacheService(IDistributedCache distributedCache) : ICacheService
{
    private readonly DistributedCacheEntryOptions Default = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    public async Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await distributedCache.GetStringAsync(key, cancellationToken);

        T? value;
        if (!string.IsNullOrWhiteSpace(cachedValue))
        {
            value = JsonSerializer.Deserialize<T>(cachedValue);

            if (value is not null)
            {
                return value;
            }
        }

        value = await factory(cancellationToken);

        if (value is null)
        {
            return default;
        }

        await distributedCache.SetStringAsync(
            key, 
            JsonSerializer.Serialize(value), 
            options ?? Default, 
            cancellationToken);

        return value;
    }

    public T? Get<T>(string key)
    {
        var cachedValue = distributedCache.GetString(key);

        if (string.IsNullOrWhiteSpace(cachedValue))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(cachedValue);
    }
    
    public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions? options = null, CancellationToken cancellationToken = default)
    {
        await distributedCache.SetStringAsync(
            key, 
            JsonSerializer.Serialize(value), 
            options ?? Default, 
            cancellationToken);
    }
}