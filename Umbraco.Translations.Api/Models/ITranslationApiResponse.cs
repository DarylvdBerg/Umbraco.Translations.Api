namespace Umbraco.Translations.Api.Models;

public interface ITranslationApiResponse
{
    IList<ITranslation> Result { get; init; }
    string ErrorMessage { get; init; }
}