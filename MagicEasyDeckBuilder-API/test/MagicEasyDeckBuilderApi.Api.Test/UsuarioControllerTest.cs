using FluentAssertions;
using MagicEasyDeckBuilderApi.Api.Test.Configuracao;
using MagicEasyDeckBuilderApi.Api.Test.DadosFake;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using Newtonsoft.Json;
using System.Net;
using Xunit;
using XUnitPriorityOrderer;

namespace MagicEasyDeckBuilderApi.Api.Test
{
    [Collection(nameof(MedbTestFixtureCollection))]
    [TestCaseOrderer(CasePriorityOrderer.TypeName, CasePriorityOrderer.AssembyName)]
    public class UsuarioControllerTest
    {
        private readonly MedbTestFixture _fixture;

        public UsuarioControllerTest(MedbTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact,Order(1)]
        public async Task Login_UsuarioNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            var dadosLogin = new UsuarioLoginViewModel
            {
                Email = UsuarioDadosFake.Email,
                Senha = UsuarioDadosFake.Senha
            };

            
            // Act
            var response = await _fixture.Client.PostAsync("usuario/login",_fixture.GetContent(dadosLogin));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound, "Deve retornar Not Found por não encontrar usuario cadastrado com estes login e senha");
        }


        [Fact,Order(2)]
        public async Task Cadastrar_UsuarioValidoInformado_DeveRetornarOk()
        {
            // Arrange
            var usuario = new UsuarioCadastroViewModel
            {
                Nome = UsuarioDadosFake.Nome,
                Email = UsuarioDadosFake.Email,
                Senha = UsuarioDadosFake.Senha,
                SenhaConfirmacao = UsuarioDadosFake.Senha
            };
            // Act
            var response = await _fixture.Client.PostAsync("usuario/cadastro", _fixture.GetContent(usuario));
            var content = await response.Content.ReadAsStringAsync();

            var usuarioRetorno = JsonConvert.DeserializeObject<UsuarioAuthViewModel>(content);

            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            usuarioRetorno.Should().NotBeNull();
        }

        [Fact, Order(3)]
        public async Task Cadastrar_UsuarioComEmailJáCadastrado_DeveRetornarOk()
        {
            // Arrange
            var usuario = new UsuarioCadastroViewModel
            {
                Nome = UsuarioDadosFake.Nome,
                Email = UsuarioDadosFake.Email,
                Senha = UsuarioDadosFake.Senha,
                SenhaConfirmacao = UsuarioDadosFake.Senha
            };
            // Act
            var response = await _fixture.Client.PostAsync("usuario/cadastro", _fixture.GetContent(usuario));
            var message = await response.Content.ReadAsStringAsync();
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            message.Should().Be("E-mail já está em uso");
        }

        [Fact, Order(4)]
        public async Task Login_UsuarioExiste_DeveRetornarOk()
        {
            // Arrange
            var dadosLogin = new UsuarioLoginViewModel
            {
                Email = UsuarioDadosFake.Email,
                Senha = UsuarioDadosFake.Senha
            };


            // Act
            var response = await _fixture.Client.PostAsync("usuario/login", _fixture.GetContent(dadosLogin));

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }


    }
}
