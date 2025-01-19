using AutoMapper;
using Umbraco.Cms.Core.Models;
using Umbraco.Translations.Api.Models;

namespace Umbraco.Translations.Api.Mappers;

/// <summary>
/// Mapping profile for converting IDictionaryTranslation to ITranslation
/// </summary>
public class TranslationMapper : Profile
{
    public TranslationMapper()
    {
        CreateMap<IDictionaryTranslation, Translation>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => src.Key))
            .ForMember(src => src.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(src => src.Culture, opt => opt.MapFrom(src => src.LanguageIsoCode));

        CreateMap<IDictionaryTranslation, ITranslation>().As<Translation>();
        
        // Map IEnumerable<Source> to IList<Target>
        CreateMap<IEnumerable<IDictionaryTranslation>, IList<Translation>>()
            .ConvertUsing((src, dest, context) =>
                src.Select(item => context.Mapper.Map<Translation>(item)).ToList());
        CreateMap<IEnumerable<IDictionaryTranslation>, IList<ITranslation>>()
            .ConvertUsing((src, dest, context) =>
                src.Select(item => context.Mapper.Map<ITranslation>(item)).ToList());
    }
}