using System.Collections.Concurrent;
using Providers.Models;

namespace Providers.Services;

public class ProviderCache(ITimeService timeService) : IProviderCache
{

    private class CacheItem
    {
        public Provider Provider { get; set; }
        public DateTime CacheTime { get; set; }
    }
    private TimeSpan CacheDuration = new TimeSpan(0, 5, 0);
    private ConcurrentDictionary<string, CacheItem> ProviderStore = new ConcurrentDictionary<string, CacheItem>();
    public Provider? GetProvider(string id)
    {
        CacheItem? result;
        var cached = ProviderStore.TryGetValue(id, out result);
        if (!cached || result?.CacheTime < timeService.Now() - CacheDuration) return null;
        return result?.Provider;
    }

    public void PutProvider(Provider provider)
    {
        ProviderStore.AddOrUpdate(
            provider.ProviderId,
            new CacheItem() { Provider = provider, CacheTime = timeService.Now() },
            (key, oldValue) => new CacheItem() { Provider = provider, CacheTime = timeService.Now() }
            );
    }
}

