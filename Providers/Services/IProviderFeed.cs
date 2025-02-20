using Providers.Models;

namespace Providers.Services;

public interface IProviderFeed {
        public Task<Provider> GetProvider(string id);
}