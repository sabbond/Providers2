namespace ProvidersTest;
using Providers.Services;
using Providers.Models;
using Moq;
using FluentAssertions;

[TestClass]
public class ProvidersServiceTests
{
    private Mock<IProviderCache> cacheMock = new Mock<IProviderCache>();
    private Mock<IProviderFeed> feedMock = new Mock<IProviderFeed>();
    static string providerId1 = "id_00001";
    private Provider provider1 = new Provider()
    {
        ProviderId = providerId1,
        Name = "test provider 1",
        LocationIds = []
    };

    [TestMethod]
    public async Task ProviderService_ReturnsCachedProvider()
    {
        cacheMock.Setup(cache => cache.GetProvider(providerId1)).Returns(provider1);
        var service = new ProviderService(feedMock.Object, cacheMock.Object);
        var response = await service.GetProvider(providerId1);
        response.Should().Be(provider1);
    }

    [TestMethod]
    public async Task ProviderService_ReturnsFromFeedIfNothingInCache()
    {
        cacheMock.Setup(cache => cache.GetProvider(providerId1)).Returns<Provider>(null);
        feedMock.Setup(feed => feed.GetProvider(providerId1).Result).Returns(provider1);
        var service = new ProviderService(feedMock.Object, cacheMock.Object);
        var response = await service.GetProvider(providerId1);
        response.Should().Be(provider1);
    }

    [TestMethod]
    public async Task ProviderService_StoresFeedResultInCache()
    {
        cacheMock.Setup(cache => cache.GetProvider(providerId1)).Returns<Provider>(null);
        feedMock.Setup(feed => feed.GetProvider(providerId1).Result).Returns(provider1);
        var service = new ProviderService(feedMock.Object, cacheMock.Object);
        var response = await service.GetProvider(providerId1);
        cacheMock.Verify(mock => mock.PutProvider(provider1), Times.Once());
    }
}