using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Services;

public interface ITranslationService
{
    /// <summary>
    /// Gets a translation by culture.
    /// </summary>
    /// <param name="culture">Umbraco culture</param>
    /// <param name="key">Translation dictionary key</param>
    /// <returns>Instance of <see cref="ITranslation"/></returns>
    ITranslation? GetTranslationByCulture(string culture, string key);
}