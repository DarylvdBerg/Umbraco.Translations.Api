using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Translations.Api.Builder;
using Umbraco.Translations.Api.Models;
using Umbraco.Translations.Api.Services;
using Umbraco.Translations.Api.Strategy;

namespace Umbraco.Translations.Api.Api;

[ApiController]
[Route("api/v{version:apiVersion}/translations")]
[ApiVersion(Constants.Api.Version)]
[Produces("application/json")]
public class TranslationApiController : ControllerBase
{
    private readonly ITranslationService _translationService;
    private readonly ICacheStrategy<ITranslation> _translationCache;
    
    public TranslationApiController(ITranslationService translationService, ICacheStrategy<ITranslation> translationCache)
    {
        _translationService = translationService;
        _translationCache = translationCache;
    }

    [HttpGet]
    public async Task<ITranslationApiResponse> Get(string culture, string key)
    {
        var responseBuilder = new TranslationApiResponseBuilder();
        var translation = await _translationCache.ExecuteCacheStrategy([key, culture],
            () => _translationService.GetTranslationByCulture(culture, key));
        
        if (translation is not null)
        {
            responseBuilder.WithResult(translation);
        }
        
        var response = responseBuilder.Build();
        return response;
    }

    [HttpGet("all")]
    public ITranslationApiResponse GetAll(string culture)
    {
        var responseBuilder = new TranslationApiResponseBuilder();
        var translations = _translationService.GetAllTranslationsByCulture(culture);

        if (translations is not null && translations.Any())
        {
            responseBuilder.WithResults(translations);
        }
        
        var response = responseBuilder.Build();
        return response;
    }
}