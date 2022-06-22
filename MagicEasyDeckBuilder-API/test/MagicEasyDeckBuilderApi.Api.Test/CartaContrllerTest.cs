using FluentAssertions;
using MagicEasyDeckBuilderApi.Api.Test.Configuracao;
using MagicEasyDeckBuilderAPI.App.ViewModel;
using Xunit;

namespace MagicEasyDeckBuilderApi.Api.Test
{
    [Collection(nameof(MedbTestFixtureCollection))]
    public class CartaContrllerTest
    {
        private readonly MedbTestFixture _fixture;

        public CartaContrllerTest(MedbTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task BuscarCartas_BuscaSemRetorno_DeveRetornarOk()
        {
            // Arrange
            var body = new FiltroBuscaCartaViewModel
            {
                Nome = "cartaquenaoexiste"
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "cartas")
            {
                Content = _fixture.GetContent(body)
            };
            // Act
            var httpResponse = await _fixture.Client.SendAsync(httpRequest);

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task BuscarCartasPorNome_BuscaSemRetorno_DeveRetornarOk()
        {
            // Arrange

            // Act
            var httpResponse = await _fixture.Client.GetAsync("cartas/cartaquenaoexiste");

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
        }


        [Fact]
        public async Task BuscarNomesDeCarta_BuscaSemRetorno_DeveRetornarOk()
        {
            // Arrange

            // Act
            var httpResponse = await _fixture.Client.GetAsync("cartas/nomes/cartaquenaoexiste");

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
