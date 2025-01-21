using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Translations.Api.Services;

public class UmbracoLocalizationWrapperService : IUmbracoLocalizationWrapperService
{
    private readonly ILocalizationService _localizationService;

    public UmbracoLocalizationWrapperService(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    
    /// <inheritdoc />
    public IDictionaryTranslation? GetDictionaryTranslation(string key, string culture)
    {
        var umbracoTranslation = _localizationService.GetDictionaryItemByKey(key);
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