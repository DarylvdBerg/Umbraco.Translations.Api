namespace Umbraco.Translations.Api.Services;

public interface ICacheKeyBuilder
{
    /// <summary>
    /// Gets the set cache prefix.
    /// </summary>
    /// <returns></returns>
    string GetCacheKeyPrefix();
    
    /// <summary>
    /// Builds the cachekey based on prefix and passed parts.
    /// </summary>
    /// <param name="parts"></param>
    /// <returns></returns>
    string BuildCacheKey(string[] parts);
}