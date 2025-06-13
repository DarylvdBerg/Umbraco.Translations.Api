using Umbraco.Translations.Services.Extensions;
using Umbraco.Translations.Services.Models;

namespace Umbraco.Translations.Services;

public class TranslationService(IUmbracoLocalizationWrapperService localizationService) : ITranslationService
{
    /// <inheritdoc />
    public ITranslation? GetTranslationByCulture(string culture, string key)
    {
       var umbracoTranslationByCulture = localizationService.GetDictionaryTranslation(key, culture);

        if (umbracoTranslationByCulture is null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(umbracoTranslationByCulture.Value))
        {
            return null;
        }

        return umbracoTranslationByCulture.ToTranslation();
    }
}