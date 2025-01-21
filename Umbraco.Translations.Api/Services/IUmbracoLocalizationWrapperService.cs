using Umbraco.Cms.Core.Models;

namespace Umbraco.Translations.Api.Services;

/// <summary>
/// Wrapper service for Umbraco's localization service
/// </summary>
public interface IUmbracoLocalizationWrapperService
{
    /// <summary>
    /// Gets a instance of <see cref="IDictionaryTranslation"/> from umbraco's Localization service
    /// </summary>
    /// <param name="key"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    IDictionaryTranslation? GetDictionaryTranslation(string key, string culture);
}