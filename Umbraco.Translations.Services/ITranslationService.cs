using Umbraco.Translations.Services.Models;

namespace Umbraco.Translations.Services;

public interface ITranslationService
{
    /// <summary>
    /// Gets a translation by culture.
    /// </summary>
    /// <param name="culture">Umbraco culture</param>
    /// <param name="key">Translation dictionary key</param>
    /// <returns>Instance of <see cref="ITranslation"/></returns>
    Task<ITranslation?> GetTranslationByCultureAsync(string culture, string key);
}