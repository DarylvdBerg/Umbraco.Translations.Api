using Microsoft.Extensions.DependencyInjection;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterPackageDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());   
    }

    public static void RegisterPackageServices(this IServiceCollection services)
    {
        services.AddTransient<ITranslationService, TranslationService>();
    }
}