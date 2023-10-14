using MagicEasyDeckBuilderAPI.Core.ViewModel;

namespace MagicEasyDeckBuilderAPI.App.Services
{
    public interface ICartaAppService
    {
        Task<ConsultaResponseViewModel> BuscaCartas(FiltroBuscaCartaViewModel filtros);
        Task<IEnumerable<string>> BuscaNomesDeCarta(string nome);
        Task<CartaViewModel> BuscaCartaPorNome(string nome);
        Task<IEnumerable<TipoViewModel>> BuscaTipos();
        Task<IEnumerable<EdicaoViewModel>> BuscaEdicoes();
    }
}