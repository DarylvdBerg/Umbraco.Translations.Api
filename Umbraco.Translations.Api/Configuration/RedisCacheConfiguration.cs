namespace Umbraco.Translations.Api.Configuration;

public class RedisCacheConfiguration
{
    /// <summary>
    ///  Section name to use when configuring the redis cache for the translation API.
    /// </summary>
    public const string RedisCacheConfigurationSectionName = "TranslationApi:RedisCacheConfiguration";

    /// <summary>
    ///  Configuration for connecting to redis.
    /// </summary>
    public string ConnectionString { get; init; }
    
    /// <summary>
    /// Configuration for specifying each entry time to live.
    /// </summary>
    public int TimeToLive { get; init; }
}