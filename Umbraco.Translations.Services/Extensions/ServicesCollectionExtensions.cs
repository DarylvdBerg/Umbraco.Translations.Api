using Microsoft.Extensions.DependencyInjection;

namespace Umbraco.Translations.Services.Extensions;

public static class ServicesCollectionExtensions
{
    public static void RegisterTranslationServices(this IServiceCollection services)
    {
        services.AddTransient<ITranslationService, TranslationService>();
        services.AddTransient<IUmbracoLocalizationWrapperService, UmbracoLocalizationWrapperService>();
    }
}