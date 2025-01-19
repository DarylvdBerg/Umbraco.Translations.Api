namespace Umbraco.Translations.Api.Models;

public interface ITranslation
{
    /// <summary>
    /// Gets the translation Guid
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// Gets the translation value
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Gets the translation culture
    /// </summary>
    public string Culture { get; }
}