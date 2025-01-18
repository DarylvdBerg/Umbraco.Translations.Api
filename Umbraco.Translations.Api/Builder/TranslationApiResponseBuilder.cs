using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Builder;

/// <summary>
/// Builder class for creating and extending an Translation ApiResponse.
/// </summary>
public class TranslationApiResponseBuilder
{
    private IList<ITranslation> _result = new List<ITranslation>();

    /// <summary>
    /// Set Results for multiple results. property
    /// </summary>
    /// <param name="result"></param>
    /// <returns>TranslationApiResponseBuilder</returns>
    public TranslationApiResponseBuilder WithResults(IList<ITranslation> result)
    {
        _result = result;
        return this;
    }

    /// <summary>
    /// Append single result item to list.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public TranslationApiResponseBuilder WithResult(ITranslation result)
    {
        _result.Add(result);
        return this;
    }

    public ITranslationApiResponse Build()
    {
        var translationApiResponse = new TranslationApiResponse()
        {
            Result = _result
        };
        
        return translationApiResponse;
    }
}