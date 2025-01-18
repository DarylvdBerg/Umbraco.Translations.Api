namespace Umbraco.Translations.Api.Models;

public interface ITranslation
{
    public string Key { get; }
    public string Value { get; }
    public string Culture { get; }
}