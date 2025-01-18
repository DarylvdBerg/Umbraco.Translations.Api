using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Api;

[ApiController]
[Route("api/v{version:apiVersion}/translations")]
[ApiVersion(Constants.Api.Version)]
public class TranslationApiController : ControllerBase
{
    public TranslationApiController()
    {
        
    }

    [HttpGet]
    public ITranslation Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet("all")]
    public IList<ITranslation> GetAll()
    {
        throw new NotImplementedException();
    }
}