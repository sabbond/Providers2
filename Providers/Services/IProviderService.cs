using Providers.Models;

namespace Providers.Services;

public interface IProviderService {
    public Task<Provider> GetProvider(string id);
}