namespace Umbraco.Translations.Services.Models;

public class Translation: ITranslation
{
    public Guid Id { get; init; }
    public string Value { get; init; }
    public string Culture { get; init; }
}