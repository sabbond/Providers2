using Providers.Models;
using System.Web;

namespace Providers.Services;

public class ProviderFeed : IProviderFeed
{
    public async Task<Provider> GetProvider(string id)
    {
        var httpClient = new HttpClient();
        var url = $"https://api.service.cqc.org.uk/public/v1/providers/{HttpUtility.UrlEncode(id)}";
//        var url = $"https://api.service.cqc.org.uk/public/v1/providers";
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "65907e17e06440f6b212ded670f54cbb");
        return await httpClient.GetFromJsonAsync<Provider>(url);
    }
}