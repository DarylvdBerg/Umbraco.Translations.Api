using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Umbraco.Translations.Api.Configuration;
using Umbraco.Translations.Api.Middleware;

namespace Umbraco.Translations.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureApiMiddleware(this WebApplication app)
    {
        var configuration = app.Configuration.GetSection(TranslationApiConfiguration.SectionName).Get<TranslationApiConfiguration>();
        if (string.IsNullOrWhiteSpace(configuration?.ApiKey))
        {
            return;
        }
        
        app.UseMiddleware<TranslationApiMiddleware>();
    }
}