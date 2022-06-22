using MagicEasyDeckBuilderAPI.App.Services;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MagicEasyDeckBuilderAPI.App.Test
{
    public class UsuarioAppServiceTest
    {
        private readonly UsuarioAppService _appService;
        private readonly AutoMocker _mocker;

        public UsuarioAppServiceTest()
        {
            _mocker = new AutoMocker();
            _appService = _mocker.CreateInstance<UsuarioAppService>();
        }


        [Fact]
        public async Task Cadastrar_EmailEmUso_RetornaErro()
        {
            // Arrange
            var email = "teste@teste.com";
            _mocker.GetMock<IUsuarioRepository>()
                .Setup(r => r.ObtemUsuarioPorEmail(email))
                .ReturnsAsync(new Usuario
                {
                    Email = email
                });
            // Act
            var result = await _appService.Cadastrar("nome", email, "senha");
            // Assert
            Assert.False(result.Sucesso);
            Assert.Equal("E-mail já está em uso",result.Mensagem);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Cadastrar(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()),Times.Never);
        }

        [Fact]
        public async Task Cadastrar_DadosCorretos_RetornaSucesso()
        {
            // Arrange
            _mocker.GetMock<IUsuarioRepository>()
                .Setup(r => r.ObtemUsuarioPorEmail(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            _mocker.GetMock<IUsuarioRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .ReturnsAsync(true);
            // Act
            var result = await _appService.Cadastrar("Nome", "teste@teste.com","123456");
            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task ObterUsuario_UsuarioNaoCadastrado_RetornaNull()
        {
            // Arrange
            _mocker.GetMock<IUsuarioRepository>()
                .Setup(r => r.ObtemUsuarioPorEmailESenha(It.IsAny<string>(),It.IsAny<string>()))
                .ReturnsAsync(value: null);
            // Act
            var usuario = await _appService.ObtemUsuario("email", "senha");
            // Assert

            Assert.Null(usuario);
        }

        [Fact]
        public async Task ObterUsuario_UsuarioCadastrado_RetornaUsuario()
        {
            // Arrange
            _mocker.GetMock<IUsuarioRepository>()
                .Setup(r => r.ObtemUsuarioPorEmailESenha(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Usuario());
            //Act
            var usuario = await _appService.ObtemUsuario("email", "senha");
            // Assert

            Assert.NotNull(usuario);
        }
    }
}
