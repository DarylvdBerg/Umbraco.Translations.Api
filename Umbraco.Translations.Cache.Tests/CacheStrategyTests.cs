using Microsoft.Extensions.Logging;
using Moq;
using Umbraco.Translations.Cache.Providers;
using Umbraco.Translations.Cache.Services;
using Umbraco.Translations.Cache.Strategy;
using Umbraco.Translations.Services.Models;

namespace Umbraco.Translations.Cache.Tests;

public class CacheStrategyTests
{
    private ICacheStrategy _cacheImplementation;
    private Mock<ICache> _cacheProvider;


    [SetUp]
    public void Setup()
    {
        _cacheImplementation = new CacheStrategy(
            new Mock<ILogger<ICacheStrategy>>().Object,
            new CacheKeyBuilder()
        );
        _cacheProvider = new Mock<ICache>();
        _cacheImplementation.SetCacheStrategy(_cacheProvider.Object);
    }

    [Test]
    public void Should_Return_Item_From_Cache_If_Hit()
    {
        // Arrange
        var cachedItem = new Translation()
        {
            Culture = "en-US",
            Id = Guid.NewGuid(),
            Value = "Hello World"
        };
        
        _cacheProvider.Setup(x => x.FetchThroughCache<ITranslation>(It.IsAny<string>()))
            .Returns(cachedItem);
        
        // Act
        var result = _cacheImplementation.FetchSingleCachedItem<ITranslation>(["key"], () => null);
        
        // Assert
        Assert.NotNull(result);
        Assert.That(result.Culture, Is.EqualTo(cachedItem.Culture));
        Assert.That(result.Value, Is.EqualTo(cachedItem.Value));
        _cacheProvider.Verify(x => x.AddToCache(It.IsAny<string>(), It.IsAny<Translation>()), Times.Never);
    }

    [Test]
    public void Should_Return_Item_From_Fallback_Func_If_Item_Is_Not_In_Cache()
    {
        // Arrange
        var cachedItem = new Translation()
        {
            Culture = "en-US",
            Id = Guid.NewGuid(),
            Value = "Hello World"
        };
        
        var mockFunc = new Mock<Func<Translation>>();
        mockFunc.Setup(x => x())
            .Returns(cachedItem);
        _cacheProvider.Setup(x => x.FetchThroughCache<ITranslation>(It.IsAny<string>()))
            .Returns<ITranslation>(null!);

        // Act
        var result = _cacheImplementation.FetchSingleCachedItem<ITranslation>(["key"], mockFunc.Object);
        
        // Assert
        Assert.NotNull(result);
        Assert.That(result.Culture, Is.EqualTo(cachedItem.Culture));
        Assert.That(result.Value, Is.EqualTo(cachedItem.Value));
        mockFunc.Verify(x => x(), Times.Once);
    }
}