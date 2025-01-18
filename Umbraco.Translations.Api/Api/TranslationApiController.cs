using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Translations.Api.Models;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Api;

[ApiController]
[Route("api/v{version:apiVersion}/translations")]
[ApiVersion(Constants.Api.Version)]
[Produces("application/json")]
public class TranslationApiController : ControllerBase
{
    private ITranslationService _translationService;
    
    public TranslationApiController(ITranslationService translationService)
    {
        _translationService = translationService;
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(ITranslation),  statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public ITranslation? Get(string culture, string key)
    {
        var translation = _translationService.GetTranslationByCulture(culture, key);
        return translation;
    }

    [HttpGet("all")]
    public IList<ITranslation> GetAll()
    {
        throw new NotImplementedException();
    }
}