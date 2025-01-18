using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Services;

public interface ITranslationService
{
    ITranslation? GetTranslationByCulture(string culture, string key);
    IList<ITranslation>? GetAllTranslationsByCulture(string culture);
}