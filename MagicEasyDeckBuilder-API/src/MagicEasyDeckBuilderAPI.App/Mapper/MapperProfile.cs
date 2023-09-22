using AutoMapper;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
namespace MagicEasyDeckBuilderAPI.App.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Deck, DeckViewModel>()
                .ForMember(dest => dest.Formato, map => map.MapFrom(src => src.TipoFormato.Nome))
                .ForMember(dest => dest.IdentidadeDeCor, map => map.MapFrom(src => src.MainDeck == null ? null : src.MainDeck.SelectMany(s => s.Carta.IdentidadeDeCor).Distinct()));

            
            CreateMap<Carta, CartaViewModel>()
                .ForMember(dst => dst.CustoMana, map => map.MapFrom(src => src.CustoMana.Custo));
            CreateMap<CartaDeck, CartaDeckViewModel>();
            CreateMap<CartaFace, CartaFaceViewModel>()
                .ForMember(dst => dst.CustoMana, map => map.MapFrom(src => src.CustoMana.Custo)); ;
        } 
    }
}
