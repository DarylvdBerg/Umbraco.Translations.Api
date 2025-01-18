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
        _mapper = mapper;
    }
    
    /// <inheritdoc />
    public ITranslation? GetTranslationByCulture(string culture, string key)
    {
        // Try get dictionary item from umbraco by provided key.
        var umbracoTranslation = _localizationService.GetDictionaryItemByKey(key);
        if (umbracoTranslation is null)
        {
            return null;
        }

        // Get the single instance of the configured translation by culture.
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

    /// <inheritdoc />
    public IList<ITranslation>? GetAllTranslationsByCulture(string culture)
    {
        // Fetch all translation guids.
        var translationKeyMap = _localizationService.GetDictionaryItemKeyMap();

        if (!translationKeyMap.Any())
        {
            return null;
        }
        
        // Store only the guids, cast it to an array.
        var translationGuidCollection = translationKeyMap
            .Select(translation => translation.Value)
            .ToArray();
        
        // Get all translations by culture from umbraco.
        var translations = translationGuidCollection.Select(guid => _localizationService.GetDictionaryItemById(guid));
        var translationsByCulture = translations
            .Where(trans => trans?.Translations != null)
            .SelectMany(trans =>
                trans.Translations
                    .Where(transByCulture =>
                        transByCulture.LanguageIsoCode.Equals(culture, StringComparison.InvariantCulture) &&
                        !string.IsNullOrWhiteSpace(transByCulture.Value)));

        if (!translationsByCulture.Any())
        {
            return null;
        }
        
        var mapped = _mapper.Map<IList<ITranslation>>(translationsByCulture);

        return mapped;
    }
}