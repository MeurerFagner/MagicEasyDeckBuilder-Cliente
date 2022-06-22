using AutoMapper;
using MagicEasyDeckBuilderAPI.App.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
namespace MagicEasyDeckBuilderAPI.App.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Deck, DeckViewModel>();
            CreateMap<Carta, CartaViewModel>()
                .ForMember(dst => dst.CustoMana, map => map.MapFrom(src => src.CustoMana.Custo));
            CreateMap<CartaDeck, CartaDeckViewModel>();
            CreateMap<CartaFace, CartaFaceViewModel>()
                .ForMember(dst => dst.CustoMana, map => map.MapFrom(src => src.CustoMana.Custo)); ;
        } 
    }
}
