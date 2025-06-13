using Moq;
using Umbraco.Cms.Core.Models;
using Umbraco.Translations.Services.Models;

namespace Umbraco.Translations.Services.Tests;

public class TranslationServiceTests
{
    private Mock<IUmbracoLocalizationWrapperService> _mockWrapperLocalization;
    private ITranslationService _service;
    
    [SetUp]
    public void Setup()
    {
        _mockWrapperLocalization = new Mock<IUmbracoLocalizationWrapperService>();
        _service = new TranslationService(_mockWrapperLocalization.Object);
    }

    [Test]
    public async Task Should_Return_Null_When_Translation_Not_Found()
    {
        // Arrange
        _mockWrapperLocalization.Setup(x => x.GetDictionaryTranslationAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((IDictionaryTranslation) null);
        
        // Act
        var translation = await _service.GetTranslationByCultureAsync("en-US", "non.existent.key");
        
        // Assert
        Assert.IsNull(translation);
    }

    [Test]
    public async Task Should_Return_Null_When_Translation_Value_Is_NullOrEmpty()
    {
        // Arrange
        _mockWrapperLocalization.Setup(x => x.GetDictionaryTranslationAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new DictionaryTranslation(new Language("en-US", "English"), ""));
        
        // Act
        var translation = await _service.GetTranslationByCultureAsync("en-US", "non.existent.key");
        
        // Assert
        Assert.IsNull(translation);
    }
    
    [Test]
    public async Task Should_Return_Translation_When_Found()
    {
        // Arrange
        var expectedTranslation = new DictionaryTranslation(new Language("en-US", "English"), "Hello World");
        _mockWrapperLocalization.Setup(x => x.GetDictionaryTranslationAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(expectedTranslation);
        
        // Act
        var translation = await _service.GetTranslationByCultureAsync("en-US", "greeting.hello");
        
        // Assert
        Assert.IsNotNull(translation);
        Assert.That(expectedTranslation.Value, Is.EqualTo("Hello World"));
    }
}