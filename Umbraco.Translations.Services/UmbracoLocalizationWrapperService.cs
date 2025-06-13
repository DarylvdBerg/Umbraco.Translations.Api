using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Translations.Services;

internal sealed class UmbracoLocalizationWrapperService(ILocalizationService localizationService)
    : IUmbracoLocalizationWrapperService
{
    /// <inheritdoc />
    public IDictionaryTranslation? GetDictionaryTranslation(string key, string culture)
    {
        var umbracoTranslation = localizationService.GetDictionaryItemByKey(key);
        if (umbracoTranslation is null)
        {
            return null;
        }

        // Get the single instance of the configured translation by culture.
        var umbracoTranslationByCulture = umbracoTranslation
            .Translations
            .SingleOrDefault(trans => trans.LanguageIsoCode.Equals(culture, StringComparison.InvariantCulture));

        return umbracoTranslationByCulture;
    }
}