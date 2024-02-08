using Microsoft.Extensions.Caching.Memory;
using MobDeMob.Application.Common.Interfaces;

namespace MobDeMob.Infrastructure.Repositories;

public class CacheRepository : ICacheRepository
{

    private readonly IMemoryCache _memoryCache;

    public CacheRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public Uri? GetValue(string key)
    {
        return _memoryCache.Get<Uri>(key);
    }

    public void SetKeyValue(string key, Uri uri,  TimeSpan? absoluteExpirationRelativeToNow = null)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSize(1) // Set the size of the cache entry
            .SetPriority(CacheItemPriority.High) // Set the priority of the cache entry
            .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Set a sliding expiration
        
        if (absoluteExpirationRelativeToNow.HasValue)
        {
            cacheEntryOptions.SetAbsoluteExpiration(absoluteExpirationRelativeToNow.Value);
        }

        _memoryCache.Set(key, uri, cacheEntryOptions);
    }
}