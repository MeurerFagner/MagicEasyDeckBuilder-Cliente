using MagicEasyDeckBuilderAPI.Core.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;

namespace MagicEasyDeckBuilderAPI.App.Services
{
    public interface ICartaAppService
    {
        Task<ConsultaResponseDTO> BuscaCartas(FiltroBuscaCartaViewModel filtros);
        Task<IEnumerable<string>> BuscaNomesDeCarta(string nome);
        Task<CartaViewModel> BuscaCartaPorNome(string nome);
        Task<IEnumerable<TipoViewModel>> BuscaTipos();
        Task<IEnumerable<EdicaoDTO>> BuscaEdicoes();
    }
}