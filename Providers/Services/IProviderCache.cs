using Providers.Models;

namespace Providers.Services;

public interface IProviderCache {
    public Provider? GetProvider(string id);
    public void PutProvider(Provider provider);
}