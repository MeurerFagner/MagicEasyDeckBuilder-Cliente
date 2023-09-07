using FluentAssertions;
using MagicEasyDeckBuilderApi.Api.Test.Configuracao;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnitPriorityOrderer;

namespace MagicEasyDeckBuilderApi.Api.Test
{
    [Collection(nameof(MedbTestFixtureCollection))]
    [TestCaseOrderer(CasePriorityOrderer.TypeName, CasePriorityOrderer.AssembyName)]
    public class DeckControllerTest
    {
        private readonly MedbTestFixture _fixture;
        private const string ID_SCRYFALL_MAIN = "199cde21-5bc3-49cd-acd4-bae3af6e5881"; //SOLRING
        private const string ID_SCRYFALL_SIDE = "199cde21-5bc3-49cd-acd4-bae3af6e5881"; //SOLRING
        private const string ID_SCRYFALL_MAYBE = "069b3a3e-5ac6-4920-96c1-276e5b7b9131"; //MANOLITO
        private const string ID_SCRYFALL_COMANDANTE = "25eff27a-eb58-4a95-b2df-4a341cf9bef6"; //BRUDCLAD


        public DeckControllerTest(MedbTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.SetAuthenticationToken();
        }


        [Theory, Order(1)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.CASUAL)]
        public async Task IncluirDeck_DeckInformatoCorretamente_DeveRetornarOIdDoDeck(string formato)
        {
            // Arrange
            var deck = new IncluiDeckViewModel
            {
                Nome = "Deck " + formato,
                Formato = formato
            };


            // Act
            var httpResponse = await _fixture.Client.PostAsync("deck", _fixture.GetContent(deck));
            var response = await httpResponse.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            var idValue = json["idDeck"];

            var id = Guid.Parse(idValue);

            if (formato == TiposFormatoJogo.CASUAL)
                _fixture.IdDeckTest = id;

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            id.Should().NotBeEmpty();
        }


        [Fact, Order(2)]
        public async Task Get_ExistemDecksCadastrados_DeveRetornarOsDecksDoUsuarioLogado()
        {
            // Arrange
            // Act
            var httpResponse = await _fixture.Client.GetAsync("deck");
            var content = await httpResponse.Content.ReadAsStringAsync();

            var decks = JsonConvert.DeserializeObject<IEnumerable<DeckViewModel>>(content);

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            decks.Should().HaveCount(8,"foi a quantidade de Decks Incluso no teste anterior");
        }


        [Fact, Order(3)]
        public async Task GetDeck_InformadoIdDeDeckCadastradoParOUsuario_DeveRetornarOk()
        {
            // Arrange
            // Act
            var httpResponse = await _fixture.Client.GetAsync($"deck/{_fixture.IdDeckTest}");
            var content = await httpResponse.Content.ReadAsStringAsync();            

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact, Order(4)]
        public async Task AdicionarCarta_InformadoTipoMain_DeckDeveTerACartaSalva()
        {
            var viewModel = new AdicionaCartaViewModel
            {
                IdDeck = _fixture.IdDeckTest,
                Tipo = TipoInclusaoCarta.MAIN,
                IdScryFall = ID_SCRYFALL_MAIN
            };
            // Act
            var httpResponse = await _fixture.Client.PostAsync("deck/adicionar-carta", _fixture.GetContent(viewModel));

            var json = await httpResponse.Content.ReadAsStringAsync();
            var deck = JsonConvert.DeserializeObject<DeckViewModel>(json);


            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            deck.MainDeck.Should().NotBeNull();
            deck.MainDeck.Should().Contain(c => c.Carta.IdScryfall == ID_SCRYFALL_MAIN);

        }

        [Fact, Order(5)]
        public async Task AdicionarCarta_InformadoTipoSide_DeckDeveTerACartaSalva()
        {
            var viewModel = new AdicionaCartaViewModel
            {
                IdDeck = _fixture.IdDeckTest,
                Tipo = TipoInclusaoCarta.SIDE,
                IdScryFall = ID_SCRYFALL_SIDE
            };
            // Act
            var httpResponse = await _fixture.Client.PostAsync("deck/adicionar-carta", _fixture.GetContent(viewModel));

            var json = await httpResponse.Content.ReadAsStringAsync();
            var deck = JsonConvert.DeserializeObject<DeckViewModel>(json);

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            deck.SideDeck.Should().NotBeNull();
            deck.SideDeck.Should().Contain(c => c.Carta.IdScryfall == ID_SCRYFALL_SIDE);
        }

        [Fact, Order(6)]
        public async Task AdicionarCarta_InformadoTipoMaybe_DeckDeveTerACartaSalva()
        {
            var viewModel = new AdicionaCartaViewModel
            {
                IdDeck = _fixture.IdDeckTest,
                Tipo = TipoInclusaoCarta.MAYBE,
                IdScryFall = ID_SCRYFALL_MAYBE
            };
            // Act
            var httpResponse = await _fixture.Client.PostAsync("deck/adicionar-carta", _fixture.GetContent(viewModel));

            var json = await httpResponse.Content.ReadAsStringAsync();
            var deck = JsonConvert.DeserializeObject<DeckViewModel>(json);

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            deck.MaybeDeck.Should().NotBeNull();
            deck.MaybeDeck.Should().Contain(c => c.Carta.IdScryfall == ID_SCRYFALL_MAYBE);
        }

        [Fact, Order(7)]
        public async Task AdicionarCarta_InformadoTipoComandante_DeckDeveTerACartaSalva()
        {
            var viewModel = new AdicionaCartaViewModel
            {
                IdDeck = _fixture.IdDeckTest,
                Tipo = TipoInclusaoCarta.COMANDANTE,
                IdScryFall = ID_SCRYFALL_COMANDANTE
            };
            // Act
            var httpResponse = await _fixture.Client.PostAsync("deck/adicionar-carta", _fixture.GetContent(viewModel));

            var json = await httpResponse.Content.ReadAsStringAsync();
            var deck = JsonConvert.DeserializeObject<DeckViewModel>(json);

            // Assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            deck.MainDeck.Should().NotBeNull();
            deck.MainDeck.Should().Contain(c => c.Carta.IdScryfall == ID_SCRYFALL_COMANDANTE && c.Comandante);
        }


        //[Fact, Order(8)]
        //public async Task MoverCarta_MoverSideParaMain_DeveRetornarOkEDeck()
        //{
        //    // Arrange
        //    // Act
        //    var httpResponse = await _fixture.Client.PostAsync()
        //    // Assert
        //}

        //[Fact, Order(9)]
        //public async Task MoverCarta_MoverMaybeParaMain_DeveRetornarOkEDeck()
        //{
        //    // Arrange
        //    var viewModel = new MoverCartaViewModel
        //    {
        //        IdDeck = _fixture.IdDeckTest,
        //        IdCarta = _
        //    }
        //    // Act
        //    // Assert
        //}


        //[Fact, Order(10)]
        //public async Task RemoverCarta_DeckPossuiMaisDeUmaCopiaDaCarta_DeveDiminuirEmUmDaQuantidadeDaCarta()
        //{
        //    // Arrange
        //    // Act
        //    // Assert
        //}

        //[Fact, Order(11)]
        //public async Task RemoverCarta_DeckPossuiMUmaCopiaDaCarta_DeveRemoveroObjetoCartaDeckMasNaoACartaDoBanco()
        //{
        //    // Arrange
        //    // Act
        //    // Assert
        //}

    }

}
