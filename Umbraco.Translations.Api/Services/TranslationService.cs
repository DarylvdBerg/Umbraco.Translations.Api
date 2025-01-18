using AutoMapper;
using Umbraco.Cms.Core.Services;
using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Services;

public class TranslationService : ITranslationService
{
    private ILocalizationService _localizationService;
    private IMapper _mapper;

    public TranslationService(ILocalizationService localizationService, IMapper mapper)
    {
        _localizationService = localizationService;
    }
    
    public ITranslation? GetTranslationByCulture(string culture, string key)
    {
        var umbracoTranslation = _localizationService.GetDictionaryItemByKey(key);
        if (umbracoTranslation is null)
        {
            return null;
        }

        var umbracoTranslationByCulture = umbracoTranslation
            .Translations
            .SingleOrDefault(trans => trans.LanguageIsoCode.Equals(culture, StringComparison.InvariantCulture));

        if (umbracoTranslationByCulture is null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(umbracoTranslationByCulture.Value))
        {
            return null;
        }
        
        var mapped = _mapper.Map<ITranslation>(umbracoTranslationByCulture);
        
        return mapped;
    }

    public IList<ITranslation>? GetAllTranslationsByCulture(string culture)
    {
        throw new NotImplementedException();
    }
}