namespace Umbraco.Translations.Api.Models;

public interface ITranslation
{
    /// <summary>
    /// Gets the translation dictionary key
    /// </summary>
    public string Key { get; }
    
    /// <summary>
    /// Gets the translation value
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Gets the translation culture
    /// </summary>
    public string Culture { get; }
}