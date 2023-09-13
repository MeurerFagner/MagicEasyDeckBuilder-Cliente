using Blazored.LocalStorage;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using Microsoft.JSInterop;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private AuthenticationStateProvider _stateProvider;
        private ILocalStorageService _localStorageService;

        public UsuarioService(HttpClient httpClient, AuthenticationStateProvider stateProvider, ILocalStorageService localStorageService) : base(httpClient)
        {
            _stateProvider = stateProvider;
            _localStorageService = localStorageService;
        }

        public async Task CadastrarUsuario(UsuarioCadastroViewModel dadosCadastro)
        {
             await PostWithSucessReturn<UsuarioCadastroViewModel, UsuarioAuthViewModel>("usuario/cadastro", dadosCadastro);
        }

        public async Task LogarUsuario(UsuarioLoginViewModel model)
        {
            var usuarioLogado = await PostWithSucessReturn<UsuarioLoginViewModel,UsuarioAuthViewModel>("usuario/login", model);

            await _localStorageService.SetItemAsync("token", usuarioLogado.Token);

            await _stateProvider.GetAuthenticationStateAsync();
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("token");

            await _stateProvider.GetAuthenticationStateAsync();
        }

    }
}
