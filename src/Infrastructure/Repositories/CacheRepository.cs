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

    public void SetKeyValye(string key, Uri uri)
    {
        _memoryCache.Set(key, uri);
    }
}