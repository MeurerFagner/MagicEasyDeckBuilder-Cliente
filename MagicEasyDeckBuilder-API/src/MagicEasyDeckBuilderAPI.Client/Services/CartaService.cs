using MagicEasyDeckBuilderAPI.Core.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public class CartaService : BaseService, ICartaService
    {
        private const string CARTA_CONTROLLER = "cartas";
        public CartaService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<CartaViewModel?> BuscaCartaPorNome(string nome)
        {
            return await _httpClient.GetFromJsonAsync<CartaViewModel>($"{CARTA_CONTROLLER}/{nome}");
        }

        public async Task<ConsultaResponseViewModel?> BuscaCartas(FiltroBuscaCartaViewModel filtros)
        {
            return await PostWithSucessReturn<FiltroBuscaCartaViewModel, ConsultaResponseViewModel>(CARTA_CONTROLLER+ "/buscar-cartas", filtros);
        }

        public async Task<IEnumerable<EdicaoViewModel>?> BuscaEdicoes()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<EdicaoViewModel>>($"{CARTA_CONTROLLER}/edicoes");
        }

        public async Task<IEnumerable<string>?> BuscaNomesDeCarta(string nome)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{CARTA_CONTROLLER}/nome/{nome}");
        }

        public async Task<IEnumerable<TipoViewModel>?> BuscarTipos()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TipoViewModel>>($"{CARTA_CONTROLLER}/tipos");
        }
    }
}
