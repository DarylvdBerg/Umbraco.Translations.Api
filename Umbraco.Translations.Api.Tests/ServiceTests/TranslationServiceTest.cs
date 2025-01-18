using Moq;
using Umbraco.Cms.Core.Services;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Tests.ServiceTests;

[TestFixture]
public class TranslationServiceTest
{
    private ITranslationService _service;
    private Mock<ILocalizationService> _localizationServiceMock;

    [SetUp]
    public void Setup()
    {
    }

    public void Should_Get_Translation_By_Culture()
    {
        
    }

    public void Should_Get_All_Translations_By_Culture()
    {
        
    }

    public void Should_Return_Null_If_Tranlsation_By_Culture_Not_Found()
    {
        
    }
}