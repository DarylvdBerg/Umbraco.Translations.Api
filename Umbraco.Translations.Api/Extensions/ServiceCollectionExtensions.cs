using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Umbraco.Translations.Api.Cache;
using Umbraco.Translations.Api.Configuration;
using Umbraco.Translations.Api.Enums;
using Umbraco.Translations.Api.Mappers;
using Umbraco.Translations.Api.Models;
using Umbraco.Translations.Api.Services;
using Umbraco.Translations.Api.Strategy;

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
        services.AddKeyedSingleton<ICache<ITranslation>, RedisCache<ITranslation>>(CacheStrategyEnum.RedisCacheStrategy);
        services.AddKeyedSingleton<ICache<ITranslation>, UmbracoCache<ITranslation>>(CacheStrategyEnum.UmbracoCacheStrategy);
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

        services.ConfigureCacheStrategy(translationApiConfiguration.CacheStrategy);
        services.ConfigureSwagger();
    }

    /// <summary>
    /// Configuration for the Translations API Swagger
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureSwagger(this IServiceCollection services)
    {
        // TODO: Check if we can move the translation API to a seperate swagger document.
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("X-Api-Key", new OpenApiSecurityScheme
            {
                Name = "X-Api-Key",
                Description = "X-Api-Key Value",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "X-Api-Key"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    /// <summary>
    /// Configure the registered cache strategy from configuration.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="cacheStrategy"></param>
    /// <exception cref="NotImplementedException"></exception>
    private static void ConfigureCacheStrategy(this IServiceCollection services, CacheStrategyEnum configuredCache)
    {
        services.AddSingleton<ICacheStrategy<ITranslation>>(sp =>
        {
            var cache = sp.GetRequiredKeyedService<ICache<ITranslation>>(configuredCache);
            var logger = sp.GetRequiredService<ILogger<CacheStrategy<ITranslation>>>();
            var cacheStrategy = new CacheStrategy<ITranslation>(logger);
            cacheStrategy.SetCacheStrategy(cache);
            return cacheStrategy;
        });
    }
}