using MagicEasyDeckBuilderAPI.App.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;

namespace MagicEasyDeckBuilderAPI.App.Services
{
    public interface IDeckAppService
    {
        Task<IEnumerable<DeckViewModel>> ObterDecksPorUsuario(Guid idUser);
        Task<Deck> ObterDeckPorId(Guid idDeck);
        Task<Guid> IncluirDeck(Guid idUsuario, string nome, string formato);
        Task<DeckViewModel> AdicionarCarta(Guid idUsuario, Guid idDeck, string idScryFall, string tipo);
        Task AlterarCapa(Guid idDeck, string urlImagem);
        Task<DeckViewModel> MoverCarta(Guid idDeck, Guid idCarta, string tipoDeckOrigem, string tipodeckDestino, Guid idUsuario);
        Task<DeckViewModel> RemoverCarta(Guid idDeck, Guid idCarta, string tipoDeck, Guid idUsuario);
    }
}