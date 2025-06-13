using Umbraco.Cms.Core.Models;
using Umbraco.Translations.Services.Extensions;

namespace Umbraco.Translations.Services.Tests;

public class TranslationExtensionsTests
{
    [Test]
    public void Should_Convert_DictionaryTranslation_To_TranslationModel()
    {
        // Arrange
        IDictionaryTranslation translation = new DictionaryTranslation(new Language("en-US", "English (United States)"), "Hello World");
        // Act
        var model = translation.ToTranslation();

        // Assert
        Assert.IsNotNull(model);
        Assert.That(model.Culture, Is.EqualTo("en-US"));
        Assert.That(model.Value, Is.EqualTo("Hello World"));
    }
}