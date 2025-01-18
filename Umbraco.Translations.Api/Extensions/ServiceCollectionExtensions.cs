using Microsoft.Extensions.DependencyInjection;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register all dependencies and services for the Translation API package.
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterTranslationsApi(this IServiceCollection services)
    {
        services.RegisterPackageDependencies();
        services.RegisterPackageServices();
    }
    
    /// <summary>
    /// Register third party dependencies.
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterPackageDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());   
    }

    /// <summary>
    /// Register project services
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterPackageServices(this IServiceCollection services)
    {
        services.AddTransient<ITranslationService, TranslationService>();
    }
}