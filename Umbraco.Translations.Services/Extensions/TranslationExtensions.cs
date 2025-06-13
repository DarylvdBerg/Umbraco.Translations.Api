using Umbraco.Cms.Core.Models;
using Umbraco.Translations.Services.Models;

namespace Umbraco.Translations.Services.Extensions;

internal static class TranslationExtensions
{
    public static Translation ToTranslation(this IDictionaryTranslation translation)
    {
        return new Translation
        {
            Id = translation.Key,
            Value = translation.Value,
            Culture = translation.LanguageIsoCode
        };
    }
}