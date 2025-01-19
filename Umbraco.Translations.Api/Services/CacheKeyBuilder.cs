using System.Reflection;

namespace Umbraco.Translations.Api.Services;

public class CacheKeyBuilder: ICacheKeyBuilder
{
    /// <inheritdoc />
    public string GetCacheKeyPrefix()
    {
        var executingApplicationName = AppDomain.CurrentDomain.FriendlyName;
        var version = Assembly.GetExecutingAssembly().ImageRuntimeVersion;
        
        return $"{executingApplicationName}:{version}:";
    }

    /// <inheritdoc />
    public string BuildCacheKey(string[] parts)
    {
        var partString = string.Join(':', parts);
        var key = string.Join(":", $"{GetCacheKeyPrefix()}{partString}");
        return key;
    }
}