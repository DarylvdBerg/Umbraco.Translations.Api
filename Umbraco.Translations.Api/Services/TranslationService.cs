using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Services;

public class TranslationService : ITranslationService
{
    private readonly IUmbracoLocalizationWrapperService _localizationService;

    public TranslationService(IUmbracoLocalizationWrapperService localizationService)
    {
        _localizationService = localizationService;
    }
    
    /// <inheritdoc />
    public ITranslation? GetTranslationByCulture(string culture, string key)
    {
       var umbracoTranslationByCulture = _localizationService.GetDictionaryTranslation(key, culture);

        if (umbracoTranslationByCulture is null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(umbracoTranslationByCulture.Value))
        {
            return null;
        }

        return new Translation();
    }
}