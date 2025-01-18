using AutoMapper;
using Umbraco.Cms.Core.Models;
using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Mappers;

public class TranslationMapper : Profile
{
    public TranslationMapper()
    {
        CreateMap<IDictionaryTranslation, Translation>()
            .ForMember(src => src.Key, opt => opt.MapFrom(src => src.Key))
            .ForMember(src => src.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(src => src.Culture, opt => opt.MapFrom(src => src.LanguageIsoCode));

        CreateMap<IDictionaryTranslation, ITranslation>().As<Translation>();
    }
}