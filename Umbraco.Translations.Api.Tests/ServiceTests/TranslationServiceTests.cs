using AutoMapper;
using Moq;
using Umbraco.Cms.Core.Models;
using Umbraco.Translations.Api.Mappers;
using Umbraco.Translations.Api.Models;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Tests.ServiceTests;

[TestFixture]
public class TranslationServiceTests
{
    
    private Mock<IUmbracoLocalizationWrapperService> _umbracoLocalizationWrapperService;
    private IMapper _mapper;
    private ITranslationService _translationService;
    
    [SetUp]
    public void Setup()
    {
        _umbracoLocalizationWrapperService = new Mock<IUmbracoLocalizationWrapperService>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<TranslationMapper>());
        _mapper = new Mapper(mapperConfig);
        _translationService = new TranslationService(_umbracoLocalizationWrapperService.Object, _mapper);
    }

    [Test]
    public void Should_Return_Null_If_Key_Not_Found()
    {
        // Arrange
        var key = "Application:Version:Key:Culture";
        var culture = "en-US";
        _umbracoLocalizationWrapperService
            .Setup(x => x.GetDictionaryTranslation(key, culture))
            .Returns<IDictionaryTranslation>(null);
        
        // Act
        var result = _translationService.GetTranslationByCulture(key, culture);
        
        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void Should_Return_Null_If_Culture_Not_Found()
    {
        // Arrange
        var key = "Application:Version:Key:Culture";
        var culture = "en-US";
        var dictionaryTranslationMock = new Mock<IDictionaryTranslation>();
        dictionaryTranslationMock.Setup(x => x.LanguageIsoCode).Returns("nl-NL");
        _umbracoLocalizationWrapperService
            .Setup(x => x.GetDictionaryTranslation(key, culture))
            .Returns(dictionaryTranslationMock.Object);
        
        // Act
        var result = _translationService.GetTranslationByCulture(culture, key);
        
        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void Should_Return_Null_If_Value_Is_Null()
    {
        // Arrange
        var key = "Application:Version:Key:Culture";
        var culture = "en-US";
        var dictionaryTranslationMock = new Mock<IDictionaryTranslation>();
        dictionaryTranslationMock.Setup(x => x.LanguageIsoCode).Returns("en-US");
        dictionaryTranslationMock.Setup(x => x.Value).Returns<string>(null);
        _umbracoLocalizationWrapperService
            .Setup(x => x.GetDictionaryTranslation(key, culture))
            .Returns(dictionaryTranslationMock.Object);
        
        // Act
        var result = _translationService.GetTranslationByCulture(culture, key);
        
        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void Should_Return_Translation_If_Translation_Found()
    {
        // Arrange
        var key = "Application:Version:Key:Culture";
        var culture = "en-US";
        var value = "Search";
        var dictionaryTranslationMock = new Mock<IDictionaryTranslation>();
        dictionaryTranslationMock.Setup(x => x.LanguageIsoCode).Returns(culture);
        dictionaryTranslationMock.Setup(x => x.Value).Returns(value);
        _umbracoLocalizationWrapperService
            .Setup(x => x.GetDictionaryTranslation(key, culture))
            .Returns(dictionaryTranslationMock.Object);
        
        // Act
        var result = _translationService.GetTranslationByCulture(culture, key);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<Translation>());
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Culture, Is.EqualTo(culture));
        });
    }
}