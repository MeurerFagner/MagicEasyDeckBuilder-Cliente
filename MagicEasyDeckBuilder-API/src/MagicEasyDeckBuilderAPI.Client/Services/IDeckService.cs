using MagicEasyDeckBuilderAPI.Core.ViewModel;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public interface IDeckService
    {
        Task<IEnumerable<DeckViewModel>> GetAllDecks();
        Task<DeckViewModel> ObterDeckPorId(Guid idDeck);
        Task<Guid> IncluirDeck(IncluiDeckViewModel novoDeck);
        Task<DeckViewModel> AdicionarCarta(Guid idDeck, string idScryFall, string tipo);
        Task AlterarCapa(Guid idDeck, string urlImagem);
        Task<DeckViewModel> MoverCarta(Guid idDeck, Guid idCarta, string tipoDeckOrigem, string tipodeckDestino, Guid idUsuario);
        Task<DeckViewModel> RemoverCarta(Guid idDeck, Guid idCarta, string tipoDeck, Guid idUsuario);

    }
}