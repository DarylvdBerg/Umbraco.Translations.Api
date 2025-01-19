using Umbraco.Translations.Api.Enums;

namespace Umbraco.Translations.Api.Configuration;

public class TranslationApiConfiguration
{
    /// <summary>
    /// Configuration section name to use when defining configuration in appsettings.
    /// </summary>
    public const string SectionName = "TranslationApi";
    
    /// <summary>
    /// ApiKey for accessing the translation api
    /// </summary>
    public string ApiKey { get; init; }
    
    /// <summary>
    /// Cache strategy to use with the translation API.
    /// </summary>
    public CacheStrategyEnum CacheStrategy { get; init; }
}