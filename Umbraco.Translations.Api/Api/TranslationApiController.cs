using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Translations.Api.Builder;
using Umbraco.Translations.Api.Models;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Api;

[ApiController]
[Route("api/v{version:apiVersion}/translations")]
[ApiVersion(Constants.Api.Version)]
[Produces("application/json")]
[Authorize]
public class TranslationApiController : ControllerBase
{
    private ITranslationService _translationService;
    
    public TranslationApiController(ITranslationService translationService)
    {
        _translationService = translationService;
    }

    [HttpGet]
    public ITranslationApiResponse Get(string culture, string key)
    {
        var responseBuilder = new TranslationApiResponseBuilder();
        var translation = _translationService.GetTranslationByCulture(culture, key);
        
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