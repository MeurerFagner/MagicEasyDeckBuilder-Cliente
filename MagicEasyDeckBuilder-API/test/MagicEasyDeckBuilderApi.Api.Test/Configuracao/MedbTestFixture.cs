using MagicEasyDeckBuilderApi.Api.Test.DadosFake;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MagicEasyDeckBuilderApi.Api.Test.Configuracao
{

    [CollectionDefinition(nameof(MedbTestFixtureCollection))]
    public class MedbTestFixtureCollection: ICollectionFixture<MedbTestFixture> { }
    public class MedbTestFixture : IDisposable
    {
        public readonly MEDBAppFactory Factory;
        public HttpClient Client;

        public Guid IdDeckTest;

        private string _tokenJwt;
        public MedbTestFixture()
        {
            Factory = new MEDBAppFactory();
            Client = Factory.CreateClient();
        }

        public void SetAuthenticationToken()
        {
            if (string.IsNullOrEmpty(_tokenJwt))
            {
                var usuario = new UsuarioCadastroViewModel
                {
                    Nome = UsuarioDadosFake.Nome,
                    Email = "outroemail@teste.com",
                    Senha = UsuarioDadosFake.Senha,
                    SenhaConfirmacao = UsuarioDadosFake.Senha
                };
                // Act
                var response = Client.PostAsync("usuario/cadastro", GetContent(usuario)).Result;
                var content = response.Content.ReadAsStringAsync().Result;

                var usuarioRetorno = JsonConvert.DeserializeObject<UsuarioAuthViewModel>(content);

                _tokenJwt = usuarioRetorno.Token;

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenJwt);
            }

        }

        public StringContent GetContent(object dados)
        {
            var dadosJson = JsonConvert.SerializeObject(dados);
            var content = new StringContent(dadosJson, Encoding.UTF8, mediaType: "application/json");

            return content;
        }
        public void Dispose()
        {
            Client?.Dispose();
            Factory?.Dispose();
        }



    }
}
