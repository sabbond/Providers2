using Providers.Models;

namespace Providers.Services;

public class ProviderService(IProviderFeed providerFeed, IProviderCache providerCache) : IProviderService
{
    public async Task<Provider> GetProvider(string id)
    {
        Provider? result = providerCache.GetProvider(id);
        if (result == null)
        {
            result = await providerFeed.GetProvider(id);
            if (result != null) providerCache.PutProvider(result);
        }
        return result;
    }
}