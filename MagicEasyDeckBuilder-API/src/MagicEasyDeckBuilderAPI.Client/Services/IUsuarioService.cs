using MagicEasyDeckBuilderAPI.Core.ViewModel;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioAuthViewModel> CadastrarUsuario(UsuarioCadastroViewModel dadosCadastro);
        Task<UsuarioAuthViewModel?> GetUsuarioLogado();
        Task LimparDadosLogin();
        Task<UsuarioAuthViewModel> LogarUsuario(UsuarioLoginViewModel model);
        Task RegistrarLogin(UsuarioAuthViewModel usuario);
    }
}