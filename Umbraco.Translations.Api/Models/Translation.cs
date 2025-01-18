namespace Umbraco.Translations.Api.Models;

public class Translation: ITranslation
{
    public string Key { get; init; }
    public string Value { get; init; }
    public string Culture { get; init; }
}