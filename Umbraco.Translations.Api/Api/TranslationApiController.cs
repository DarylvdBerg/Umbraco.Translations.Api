using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Translations.Api.Builder;
using Umbraco.Translations.Api.Models;
using Umbraco.Translations.Api.Services;
using Umbraco.Translations.Cache.Strategy;
using Umbraco.Translations.Services;

namespace Umbraco.Translations.Api.Api;

[ApiController]
[Route("api/v{version:apiVersion}/translations")]
[ApiVersion(Constants.Api.Version)]
[Produces("application/json")]
public class TranslationApiController(
    ITranslationService translationService,
    ICacheStrategy translationCache,
    ILogger<TranslationApiController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<ITranslationApiResponse> Get(string culture, string key)
    {
        var responseBuilder = new TranslationApiResponseBuilder();
        
        try
        {
            var translationTask = translationCache.FetchSingleCachedItem([key, culture],
                () => translationService.GetTranslationByCultureAsync(culture, key));

            if (translationTask is null)
            {
                return responseBuilder    
                    .WithErrorMessage($"No translation found for key: {key} and culture: {culture}")
                    .Build();
            }
            
            var translation = await translationTask;
        
            if (translation is not null)
            {
                responseBuilder.WithResult(translation);
            }
        }
        catch (Exception e)
        {
            var message = $"Failed to fetch single translation with provided key: {key}, and culture: {culture}";
            logger.LogError(e, message);
            responseBuilder.WithErrorMessage(message);
        }
        
        var response = responseBuilder.Build();
        return response;
    }
}