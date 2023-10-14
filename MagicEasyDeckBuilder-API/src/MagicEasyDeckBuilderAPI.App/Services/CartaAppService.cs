using AutoMapper;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
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

        public async Task<ConsultaResponseViewModel> BuscaCartas(FiltroBuscaCartaViewModel filtros)
        {
            var retornoDTO = await _scryfallApiService.BuscaCartas(
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

            var retorno = _mapper.Map<ConsultaResponseViewModel>(retornoDTO);
            return retorno;
        }

        public async Task<IEnumerable<EdicaoViewModel>> BuscaEdicoes()
        
        {
            var edicoes = await _scryfallApiService.BuscaEdicoes();
            return _mapper.Map<IEnumerable<EdicaoViewModel>>(edicoes);
        }

        public async Task<IEnumerable<string>> BuscaNomesDeCarta(string nome)
        {
            return await _scryfallApiService.BuscaNomesDeCarta(nome);
        }

        public async Task<IEnumerable<TipoViewModel>> BuscaTipos()
        {

            var tipos = new List<TipoViewModel>
            {
                new TipoViewModel("Types", TipoCarta.Tipos),
                new TipoViewModel("Supertypes", TipoCarta.SuperTipos)
            };

            var dicionarioDeBuscas = new Dictionary<string, Func<Task<IEnumerable<string>>>>();
            dicionarioDeBuscas[TipoCarta.ARTEFATO] = _scryfallApiService.BuscaTiposArtefato;
            dicionarioDeBuscas[TipoCarta.CRIATURA] = _scryfallApiService.BuscaTiposCriatura;
            dicionarioDeBuscas[TipoCarta.ENCANTAMENTO] = _scryfallApiService.BuscaTiposEncantamento;
            dicionarioDeBuscas[TipoCarta.TERRENO] = _scryfallApiService.BuscaTiposTerreno;
            dicionarioDeBuscas[TipoCarta.PLANESWALKER] = _scryfallApiService.BuscaTiposPlaneswalker;
            dicionarioDeBuscas[TipoCarta.MAGIA_E_FEITICO] = _scryfallApiService.BuscaTiposMagia;

            await Parallel.ForEachAsync(dicionarioDeBuscas, async (dado,token) =>
            {
                var retorno = await dado.Value.Invoke();
                Console.WriteLine(dado.Key);
                tipos.Add(new TipoViewModel(dado.Key, retorno));
            });

                Console.WriteLine("Fim");
            return tipos;
        }
    }
}
