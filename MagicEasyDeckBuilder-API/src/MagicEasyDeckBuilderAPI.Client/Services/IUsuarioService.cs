using MagicEasyDeckBuilderAPI.Core.ViewModel;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public interface IUsuarioService
    {
        Task CadastrarUsuario(UsuarioCadastroViewModel dadosCadastro);
        Task Logout();
        Task LogarUsuario(UsuarioLoginViewModel model);
        Task VerificaAutenticacao();
    }
}