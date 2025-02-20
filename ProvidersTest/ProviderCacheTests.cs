namespace ProvidersTest;
using Providers.Services;
using Providers.Models;
using Moq;
using FluentAssertions;

[TestClass]
public class ProviderCacheTests
{
    private Mock<ITimeService> timeMock = new Mock<ITimeService>();
    static string providerId1 = "id_00001";
    private Provider provider1 = new Provider()
    {
        ProviderId = providerId1,
        Name = "test provider 1",
        LocationIds = []
    };

    [TestMethod]
    public void ProviderCache_ReturnsCachedProvider()
    {
        timeMock.Setup(time => time.Now()).Returns(DateTime.Now);
        var cache = new ProviderCache(timeMock.Object);
        cache.PutProvider(provider1);
        var result = cache.GetProvider(providerId1);
        result.Should().Be(provider1);
    }

    [TestMethod]
    public void ProviderCache_ReturnsNullIfExpired()
    {
        var now = DateTime.Now;
        var tooOld = now.AddMinutes(-10);
        timeMock.Setup(time => time.Now()).Returns(tooOld);
        var cache = new ProviderCache(timeMock.Object);
        cache.PutProvider(provider1);
        timeMock.Setup(time => time.Now()).Returns(now);
        var result = cache.GetProvider(providerId1);
        result.Should().Be(null);
    }
}