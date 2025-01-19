namespace Umbraco.Translations.Api.Models;

public class TranslationApiResponse : ITranslationApiResponse
{
    public IList<ITranslation> Result { get; init; }
    public string ErrorMessage { get; init; }
}   