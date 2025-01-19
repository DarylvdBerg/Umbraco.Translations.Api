using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<TranslationApiController> _logger;

    public TranslationApiController(ITranslationService translationService, ICacheStrategy<ITranslation> translationCache, ILogger<TranslationApiController> logger)
    {
        _translationService = translationService;
        _translationCache = translationCache;
        _logger = logger;
    }

    [HttpGet]
    public ITranslationApiResponse Get(string culture, string key)
    {
        var responseBuilder = new TranslationApiResponseBuilder();
        
        try
        {
            var translation =  _translationCache.FetchSingleCachedItem([key, culture],
                () => _translationService.GetTranslationByCulture(culture, key));
        
            if (translation is not null)
            {
                responseBuilder.WithResult(translation);
            }
        }
        catch (Exception e)
        {
            var message = $"Failed to fetch single translation with provided key: {key}, and culture: {culture}";
            _logger.LogError(e, message);
            responseBuilder.WithErrorMessage(message);
        }
        
        var response = responseBuilder.Build();
        return response;
    }

    [HttpGet("all")]
    public ITranslationApiResponse GetAll(string culture)
    {
        var responseBuilder = new TranslationApiResponseBuilder();
        try
        {
            var translations = _translationService.GetAllTranslationsByCulture(culture);

            if (translations is not null && translations.Any())
            {
                responseBuilder.WithResults(translations);
            }
        }
        catch (Exception e)
        {
            var message = $"Failed to fetch translations for culture: {culture}";
            _logger.LogError(e, message);
            responseBuilder.WithErrorMessage(message);
        }
        
        var response = responseBuilder.Build();
        return response;
    }
}