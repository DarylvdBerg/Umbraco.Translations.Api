using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Umbraco.Translations.Api.Configuration;
using Umbraco.Translations.Api.Enums;
using Umbraco.Translations.Api.Mappers;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register all dependencies and services for the Translation API package.
    /// </summary>
    /// <param name="builder"></param>
    public static void RegisterTranslationsApi(this IHostApplicationBuilder builder)
    {
        builder.Services.RegisterPackageDependencies();
        builder.Services.RegisterPackageServices();
        builder.Services.RegisterTranslationApiConfiguration(builder.Configuration);
    }
    
    /// <summary>
    /// Register third party dependencies.
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterPackageDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TranslationMapper));
    }

    /// <summary>
    /// Register project services
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterPackageServices(this IServiceCollection services)
    {
        services.AddTransient<ITranslationService, TranslationService>();
    }

    /// <summary>
    /// Register the translation API configuration.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    private static void RegisterTranslationApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var translationApiConfiguration = configuration.GetSection(TranslationApiConfiguration.SectionName).Get<TranslationApiConfiguration>();
        if (translationApiConfiguration is null)
        {
            throw new ArgumentNullException(nameof(translationApiConfiguration), "Translation API configuration is missing");
        }
        
        // Bind configuration so we're able to use IOptionsMonitor.
        services
            .AddOptions<TranslationApiConfiguration>()
            .BindConfiguration(TranslationApiConfiguration.SectionName);  

        ConfigureCacheStrategy(translationApiConfiguration.CacheStrategy);
    }

    private static void ConfigureCacheStrategy(CacheStrategyEnum cacheStrategy)
    {
        return;
    }
}