﻿using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Builder;

/// <summary>
/// Builder class for creating and extending an Translation ApiResponse.
/// </summary>
public class TranslationApiResponseBuilder
{
    private readonly List<ITranslation> _result = new();
    private string _errorMessage = string.Empty;

    /// <summary>
    /// Set Results for multiple results. property
    /// </summary>
    /// <param name="result"></param>
    /// <returns>TranslationApiResponseBuilder</returns>
    public TranslationApiResponseBuilder WithResults(IList<ITranslation> result)
    {
        _result.AddRange(result);
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

    /// <summary>
    /// Append error message to the api response.
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public TranslationApiResponseBuilder WithErrorMessage(string errorMessage)
    {
        _errorMessage = errorMessage;
        return this;
    }

    public ITranslationApiResponse Build()
    {
        var translationApiResponse = new TranslationApiResponse()
        {
            Result = _result,
            ErrorMessage = _errorMessage
        };
        
        return translationApiResponse;
    }
}