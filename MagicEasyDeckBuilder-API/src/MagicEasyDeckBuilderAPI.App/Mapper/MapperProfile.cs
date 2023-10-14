using AutoMapper;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;

namespace MagicEasyDeckBuilderAPI.App.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Deck, DeckViewModel>()
                .ForMember(dest => dest.Formato, map => map.MapFrom(src =>
                    new FormatoViewModel(
                        src.TipoFormato.Nome,
                        src.TipoFormato.UsaComandante(),
                        src.TipoFormato.GetQuantidadeSideDeck() > 0,
                        src.TipoFormato.GetLimiteMinimoDeCartas())));


            CreateMap<EdicaoDTO, EdicaoViewModel>();
            CreateMap<ConsultaResponseDTO, ConsultaResponseViewModel>();
            CreateMap<Carta, CartaViewModel>()
                .ForMember(dst => dst.CustoMana, map => map.MapFrom(src => src.CustoMana.Custo));
            CreateMap<CartaDeck, CartaDeckViewModel>()
                .ForMember(dst => dst.TipoDeck, map => map.MapFrom(src => ConverteTipoDeck(src.TipoDeck)));
            CreateMap<CartaFace, CartaFaceViewModel>()
                .ForMember(dst => dst.CustoMana, map => map.MapFrom(src => src.CustoMana.Custo)); ;
        } 

        private string ConverteTipoDeck(TipoDeckCarta tipoDeckCarta)
        {
            return tipoDeckCarta switch
            {
                TipoDeckCarta.MainDeck => TipoInclusaoCarta.MAIN,
                TipoDeckCarta.SideDeck => TipoInclusaoCarta.SIDE,
                TipoDeckCarta.MaybeDeck => TipoInclusaoCarta.MAYBE,
                _ => "",
            };
        }
    }
}
