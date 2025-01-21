using AutoMapper;
using Umbraco.Cms.Core.Services;
using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Services;

public class TranslationService : ITranslationService
{
    private readonly IUmbracoLocalizationWrapperService _localizationService;
    private readonly IMapper _mapper;

    public TranslationService(IUmbracoLocalizationWrapperService localizationService, IMapper mapper)
    {
        _localizationService = localizationService;
        _mapper = mapper;
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
        
        var mapped = _mapper.Map<ITranslation>(umbracoTranslationByCulture);
        
        return mapped;
    }
}