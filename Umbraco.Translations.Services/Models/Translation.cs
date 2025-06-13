namespace Umbraco.Translations.Services.Models;

public class Translation: ITranslation
{
    public Guid Id { get; init; }
    public required string Value { get; init; }
    public required string Culture { get; init; }
}