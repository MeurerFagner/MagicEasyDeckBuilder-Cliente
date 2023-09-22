using MagicEasyDeckBuilderAPI.Core;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public class DeckService : BaseService, IDeckService
    {
        private const string DECK_CONTROLLER = "deck";

        public DeckService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<DeckViewModel>> GetAllDecks()
        {
            var decks = await _httpClient.GetFromJsonAsync<IEnumerable<DeckViewModel>>(DECK_CONTROLLER);

            return decks!;
        }

        public async Task<DeckViewModel> ObterDeckPorId(Guid idDeck)
        {
            var deck = await _httpClient.GetFromJsonAsync<DeckViewModel>($"{DECK_CONTROLLER}/{idDeck}");

            return deck!;
        }

        public async Task<Guid> IncluirDeck(IncluiDeckViewModel novoDeck)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync<IncluiDeckViewModel>(DECK_CONTROLLER, novoDeck);
            var result = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception("Erro ao incluir o Deck: " + result);

            var jsonObj = JsonSerializer.Deserialize<IncluirDeckRetornoViewModel>(result);

            return jsonObj.IdDeck;
        }

        public async Task<DeckViewModel> AdicionarCarta(Guid idDeck, string idScryFall, string tipo)
        {
            var addCartaviewModel = new AdicionaCartaViewModel(idDeck,idScryFall,tipo);
            var deck = await PostWithSucessReturn<AdicionaCartaViewModel, DeckViewModel>($"{DECK_CONTROLLER}", addCartaviewModel);

            return deck;
        }

        public Task AlterarCapa(Guid idDeck, string urlImagem)
        {
            throw new NotImplementedException();
        }

        public Task<DeckViewModel> MoverCarta(Guid idDeck, Guid idCarta, string tipoDeckOrigem, string tipodeckDestino, Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<DeckViewModel> RemoverCarta(Guid idDeck, Guid idCarta, string tipoDeck, Guid idUsuario)
        {
            throw new NotImplementedException();
        }

    }
}
