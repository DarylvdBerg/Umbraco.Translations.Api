using System.Reflection;
using Umbraco.Translations.Cache.Services;

namespace Umbraco.Translations.Cache.Tests;

public class CacheKeyBuilderTests
{
    [Test]
    public void Should_Return_CacheKey_Including_Assembly_Prefix()
    {
        // Arrange
        var cacheKeyBuilder = new CacheKeyBuilder();
        var expectedCacheKey =
            $"{AppDomain.CurrentDomain.FriendlyName}:{Assembly.GetExecutingAssembly().ImageRuntimeVersion}:a:b:c";
        
        // Act
        var result = cacheKeyBuilder.BuildCacheKey(["a", "b", "c"]);
        
        // Assert
        Assert.That(result, Is.EqualTo(expectedCacheKey));
    }
}