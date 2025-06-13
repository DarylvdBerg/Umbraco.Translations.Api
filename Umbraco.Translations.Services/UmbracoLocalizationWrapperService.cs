using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Translations.Services;

internal sealed class UmbracoLocalizationWrapperService(IDictionaryItemService dictionaryItemService)
    : IUmbracoLocalizationWrapperService
{
    /// <inheritdoc />
    public async Task<IDictionaryTranslation?> GetDictionaryTranslationAsync(string key, string culture)
    {
        var translation = await dictionaryItemService.GetAsync(key);
        if (translation is null)
        {
            return null;
        }

        // Get the single instance of the configured translation by culture.
        var umbracoTranslationByCulture = translation
            .Translations
            .SingleOrDefault(trans => trans.LanguageIsoCode.Equals(culture, StringComparison.InvariantCulture));

        return umbracoTranslationByCulture;
    }
}