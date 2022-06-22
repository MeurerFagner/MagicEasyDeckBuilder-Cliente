using AutoMapper;
using MagicEasyDeckBuilderAPI.App.ViewModel;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;

namespace MagicEasyDeckBuilderAPI.App.Services
{
    public class CartaAppService: ICartaAppService
    {
        private readonly IScryfallApiService _scryfallApiService;
        private readonly IMapper _mapper;

        public CartaAppService(IScryfallApiService scryfallApiService, IMapper mapper)
        {
            _scryfallApiService = scryfallApiService;
            _mapper = mapper;
        }

        public async Task<CartaViewModel> BuscaCartaPorNome(string nome)
        {
            var carta = await _scryfallApiService.BucaCartaPorNome(nome);

            return _mapper.Map<CartaViewModel>(carta);
        }

        public async Task<ConsultaResponseDTO> BuscaCartas(FiltroBuscaCartaViewModel filtros)
        {
            return await _scryfallApiService.BuscaCartas(
                filtros.Nome,
                filtros.Formato,
                filtros.Tipos,
                filtros.Raridade,
                filtros.Edicao,
                filtros.CustoMana,
                filtros.ValorMana,
                filtros.IdentidadeDeCor,
                filtros.FiltroDeCor,
                filtros.TipoFiltroCor,
                filtros.Texto,
                filtros.Page);

        }

        public async Task<IEnumerable<EdicaoDTO>> BuscaEdicoes()
        {
            var edicoes = await _scryfallApiService.BuscaEdicoes();
            return edicoes.OrderBy(o => o.Nome);
        }

        public async Task<IEnumerable<string>> BuscaNomesDeCarta(string nome)
        {
            return await _scryfallApiService.BuscaNomesDeCarta(nome);
        }

        public async Task<IEnumerable<TipoViewModel>> BuscaTipos()
        {
            var tipos = new List<TipoViewModel>();            

            var tiposArtefatos = await _scryfallApiService.BuscaTiposArtefato();
            var tiposCriaturas = await _scryfallApiService.BuscaTiposCriatura();
            var tiposEncantamentos = await _scryfallApiService.BuscaTiposEncantamento();
            var tiposPlaneswalker = await _scryfallApiService.BuscaTiposPlaneswalker();
            var tiposMagias = await _scryfallApiService.BuscaTiposMagia();
            var tiposTerrenos = await _scryfallApiService.BuscaTiposTerreno();

            tipos.Add(new TipoViewModel("Types", TipoCarta.Tipos));
            tipos.Add(new TipoViewModel("Supertypes", TipoCarta.SuperTipos));
            tipos.Add(new TipoViewModel(TipoCarta.ARTEFATO, tiposArtefatos));
            tipos.Add(new TipoViewModel(TipoCarta.CRIATURA, tiposCriaturas));
            tipos.Add(new TipoViewModel(TipoCarta.ENCANTAMENTO, tiposEncantamentos));
            tipos.Add(new TipoViewModel(TipoCarta.TERRENO, tiposTerrenos));
            tipos.Add(new TipoViewModel(TipoCarta.PLANESWALKER, tiposPlaneswalker));
            tipos.Add(new TipoViewModel("Spells", tiposMagias));

            return tipos;
        }
    }
}
