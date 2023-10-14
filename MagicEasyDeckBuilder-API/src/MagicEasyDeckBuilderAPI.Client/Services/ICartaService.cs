using MagicEasyDeckBuilderAPI.Core.ViewModel;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public interface ICartaService
    {
        Task<ConsultaResponseViewModel?> BuscaCartas(FiltroBuscaCartaViewModel filtros);
        Task<IEnumerable<string>?> BuscaNomesDeCarta(string nome);
        Task<CartaViewModel?> BuscaCartaPorNome(string nome);
        Task<IEnumerable<TipoViewModel>?> BuscarTipos();
        Task<IEnumerable<EdicaoViewModel>?> BuscaEdicoes();
    }
}
