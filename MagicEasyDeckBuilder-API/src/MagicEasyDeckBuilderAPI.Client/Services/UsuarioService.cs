using MagicEasyDeckBuilderAPI.Core.ViewModel;
using Microsoft.JSInterop;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private  readonly IJSRuntime _js;
        public UsuarioService(HttpClient httpClient, IJSRuntime js) : base(httpClient)
        {
            _js = js;
        }

        public async Task<UsuarioAuthViewModel> CadastrarUsuario(UsuarioCadastroViewModel dadosCadastro)
        {
            return await PostWithSucessReturn<UsuarioCadastroViewModel, UsuarioAuthViewModel>("usuario/cadastro", dadosCadastro);
        }

        public async Task RegistrarLogin(UsuarioAuthViewModel usuario)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "id", usuario.Id);
            await _js.InvokeVoidAsync("localStorage.setItem", "nome", usuario.Nome);
            await _js.InvokeVoidAsync("localStorage.setItem", "email", usuario.Email);
            await _js.InvokeVoidAsync("localStorage.setItem", "token", usuario.Token);
        }

        public async Task<UsuarioAuthViewModel> LogarUsuario(UsuarioLoginViewModel model)
        {
            return await PostWithSucessReturn<UsuarioLoginViewModel,UsuarioAuthViewModel>("usuario/login", model);
        }

        public async Task LimparDadosLogin()
        {
            await _js.InvokeVoidAsync("localStorage.clear");
        }

        public async Task<UsuarioAuthViewModel?> GetUsuarioLogado()
        {
            var nome = await _js.InvokeAsync<string>("localStorage.getItem", "nome");
            if (string.IsNullOrEmpty(nome) || nome == "null")
                return null;

            var id = await _js.InvokeAsync<Guid>("localStorage.getItem", "id");
            var email = await _js.InvokeAsync<string>("localStorage.getItem", "email");
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "token");

            return new UsuarioAuthViewModel
            {
                Id = id,
                Nome = nome,
                Email = email,
                Token = token
            };
        }

    }
}
