using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Umbraco.Translations.Api.Configuration;

namespace Umbraco.Translations.Api.Middleware;

public class TranslationApiMiddleware : ApiAuthorizationMiddlewareBase
{
    private readonly IOptionsMonitor<TranslationApiConfiguration> _options;

    protected override string ApiKeyValue => _options.CurrentValue.ApiKey;
    protected override string ApiKeyHeaderName => "X-Api-Key";
    protected override string ApiValidationPath => "api/v1.0/translations";

    public TranslationApiMiddleware(RequestDelegate next, IOptionsMonitor<TranslationApiConfiguration> options) : base(next)
    {
        _options = options;
    }
}