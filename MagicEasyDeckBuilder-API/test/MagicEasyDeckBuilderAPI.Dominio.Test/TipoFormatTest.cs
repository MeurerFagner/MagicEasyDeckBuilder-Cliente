using Bogus;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using MagicEasyDeckBuilderAPI.Dominio.Test.DadosFake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MagicEasyDeckBuilderAPI.Dominio.Test
{
    public class TipoFormatTest
    {

        #region VALIDAÇÃO DECK
        [Theory]
        [InlineData(TiposFormatoJogo.CASUAL,60)]
        [InlineData(TiposFormatoJogo.VINTAGE,60)]
        [InlineData(TiposFormatoJogo.LEGACY,60)]
        [InlineData(TiposFormatoJogo.MODERN,60)]
        [InlineData(TiposFormatoJogo.PIONEER,60)]
        [InlineData(TiposFormatoJogo.PAUPER,60)]
        [InlineData(TiposFormatoJogo.BRAWL,60)]
        [InlineData(TiposFormatoJogo.BRAWL_LIVRE,60)]
        [InlineData(TiposFormatoJogo.COMMANDER,100)]
        [InlineData(TiposFormatoJogo.COMMANDER_LIVRE,100)]
        public void ValidaDeck_DeckComCartasIgualAoLimiteDeCartas_NaoDeveRetornarErroDeLimiteminimoOuMaxiomoExcedido(string formato,int limiteQuantidadeCartas)
        {
            // Arrange
            var deck = DeckFake.GetDeckPorformato(formato, limiteQuantidadeCartas);

            // Act
            var erros = deck.TipoFormato.ValidaDeck(deck);

            // Assert
            Assert.DoesNotContain(erros.Erros, e => e.Contains(MensagemDeErro.ABAIXO_DO_LIMITE_DE_CARTAS));
            Assert.DoesNotContain(erros.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_CARTAS));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.CASUAL)]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.BRAWL_LIVRE)]
        public void ValidaDeck_DeckComMenosQueOLimiteDe60Cartas_RetornarErroEMensagemDeErro(string formato)
        {
            // Arrange
            var deck = DeckFake.GetDeckPorformato(formato, 59);

            // Act
            var erros = deck.TipoFormato.ValidaDeck(deck);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.ABAIXO_DO_LIMITE_DE_CARTAS));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        public void ValidaDeck_SideDeckComMaisQueOLimiteDe15Cartas_RetornarErroEMensagemDeErro(string formato)
        {
            // Arrange
            var deck = DeckFake.GetDeckPorformato(formato, 60, 16);

            // Act
            var erros = deck.TipoFormato.ValidaDeck(deck);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.SIDE_DECK_ACIMA_DO_LIMITE_DE_CARTAS));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.BRAWL_LIVRE)]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        [InlineData(TiposFormatoJogo.COMMANDER_LIVRE)]
        public void ValidaDeck_SideDeckNaoPodeTerCartasNoDeck_RetornarErroEMensagemDeErro(string formato)
        {
            // Arrange
            var deck = DeckFake.GetDeckPorformato(formato, 60, 1);

            // Act
            var erros = deck.TipoFormato.ValidaDeck(deck);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.SIDE_DECK_ACIMA_DO_LIMITE_DE_CARTAS));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.BRAWL_LIVRE)]
        public void ValidaDeck_DeckComMaisQueOLimiteDe60Cartas_RetornarErroEMensagemDeErro(string formato)
        {
            // Arrange
            var deck = DeckFake.GetDeckPorformato(formato, 61);

            // Act
            var erros = deck.TipoFormato.ValidaDeck(deck);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_CARTAS));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        [InlineData(TiposFormatoJogo.COMMANDER_LIVRE)]
        public void ValidaDeck_DeckComMaisQueOLimiteDe100Cartas_RetornarErroEMensagemDeErro(string formato)
        {
            // Arrange
            var deck = DeckFake.GetDeckPorformato(formato, 101);

            // Act
            var erros = deck.TipoFormato.ValidaDeck(deck);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_CARTAS));
        }


        [Theory]
        [InlineData(TiposFormatoJogo.CASUAL)]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        public void ValidaDeck_SomaDeCopiasDeUmaMesmaCartaNoDeckENoSideExcedeLimiteDeCopias_RetornarErroEMensagemDeErro(string formato)
        {
            // Arrange
            var idCarta = new Guid();
            var deck = new Deck();

            var cardDeck = new CartaDeck
            {
                IdCarta = idCarta,
                Carta = new Carta
                {
                    Id = idCarta
                },
                Quantidade = 2
            };

            var cardSideDeck = new CartaDeck
            {
                IdCarta = idCarta,
                Carta = new Carta
                {
                    Id = idCarta
                },
                Quantidade = 3
            };

            deck.Cartas.Add(cardDeck);
            deck.SideDeck.Add(cardSideDeck);

            var tipoFormato = TipoFormato.Factory(formato);

            // Act
            var erros = tipoFormato.ValidaDeck(deck);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains($"{MensagemDeErro.ACIMA_DO_LIMITE_DE_COPIAS} - 5/4"));
        }

        #endregion

        #region VALIDAÇÃO CARTA

        [Theory]
        [InlineData(TiposFormatoJogo.CASUAL, 4)]
        [InlineData(TiposFormatoJogo.VINTAGE, 4)]
        [InlineData(TiposFormatoJogo.LEGACY, 4)]
        [InlineData(TiposFormatoJogo.MODERN, 4)]
        [InlineData(TiposFormatoJogo.PIONEER, 4)]
        [InlineData(TiposFormatoJogo.PAUPER, 4)]
        [InlineData(TiposFormatoJogo.BRAWL, 1)]
        [InlineData(TiposFormatoJogo.BRAWL_LIVRE, 1)]
        [InlineData(TiposFormatoJogo.COMMANDER, 1)]
        [InlineData(TiposFormatoJogo.COMMANDER_LIVRE, 1)]
        public void ValidaCarta_LimiteDeCopiasDaCartaExcedido_RetornarErroEMensagemDeErro(string formato, int limiteDeCopias)
        {
            // Arrange
            var carta = new Carta();
            var tipoFormato = TipoFormato.Factory(formato);

            // Act
            var erros = tipoFormato.ValidaCarta(carta, limiteDeCopias + 1);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_COPIAS));
        }


        [Theory]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        public void ValidaCarta_CartaNaoLegalNoFormato_RetornaErroEMensagemDeErro(string formato)
        {
            // Arrange
            var carta = new Carta
            {
                LegalidadePorFormato = new Dictionary<string,Legalidade>()
                {
                    {formato,Legalidade.NAO_LEGAL }
                }
            };

            var tipoFormato = TipoFormato.Factory(formato);
            // Act
            var erros = tipoFormato.ValidaCarta(carta, 1);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.CARTA_NAO_LEGAL_NO_FORMATO));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        public void ValidaCarta_CartaBanidaNoFormato_RetornaErroEMensagemDeErro(string formato)
        {
            // Arrange
            var carta = new Carta
            {
                LegalidadePorFormato = new Dictionary<string, Legalidade>()
                {
                    {formato,Legalidade.BANIDO }
                }
            };

            var tipoFormato = TipoFormato.Factory(formato);
            // Act
            var erros = tipoFormato.ValidaCarta(carta, 1);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.CARTA_BANIDA_NO_FORMATO));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        public void ValidaCarta_CartaRestritoNoFormatoComMaisDeUmaCopia_RetornaErroEMensagemDeErro(string formato)
        {
            // Arrange
            var carta = new Carta
            {
                LegalidadePorFormato = new Dictionary<string, Legalidade>()
                {
                    {formato,Legalidade.RESTRITA }
                }
            };

            var tipoFormato = TipoFormato.Factory(formato);
            // Act
            var erros = tipoFormato.ValidaCarta(carta, 2);

            // Assert
            Assert.False(erros.Valido);
            Assert.Contains(erros.Erros, e => e.Contains(MensagemDeErro.CARTA_RESTRITA_NO_FORMATO));
        }

        [Theory]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        public void ValidaCarta_CartaRestritoNoFormatoComUmaCopia_NaoRetornaErroDeRestricao(string formato)
        {
            // Arrange
            var carta = new Carta
            {
                LegalidadePorFormato = new Dictionary<string, Legalidade>()
                {
                    {formato,Legalidade.RESTRITA }
                }
            };

            var tipoFormato = TipoFormato.Factory(formato);
            // Act
            var erros = tipoFormato.ValidaCarta(carta, 1);

            // Assert
            Assert.DoesNotContain(erros.Erros, e => e.Contains(MensagemDeErro.CARTA_RESTRITA_NO_FORMATO));
        }
        #endregion

        #region VALIDACAO CARTAS ESPECIAIS

        [Fact]
        public void ValidaCarta_TerrenoBasicoAcimaDoLimiteDeCopias_NaoDeveDarErroPorExcederLimiteDeCopia()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Terreno Básico — Floresta"
            };
            var tipoFormato = TipoFormato.Factory(new Faker().FormatoJogo());
            // Act
            var erros = tipoFormato.ValidaCarta(carta, 20);
            // Assert
            Assert.DoesNotContain(erros.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_COPIAS));
        }


        [Fact]
        public void ValidaCarta_CartaSeteAnoes_PodeTerAteSeteCartasNoDeck()
        {
            // Arrange
            var carta = new Carta
            {
                IdScryfall = IdCartasEspeciais.SETE_ANOES
            };

            var tipoFormato = TipoFormato.Factory(new Faker().FormatoJogo());

            // Act
            var errosComSeteCartas = tipoFormato.ValidaCarta(carta, 7);
            var errosComMaisDeSeteCartas = tipoFormato.ValidaCarta(carta, 8);

            // Assert
            Assert.DoesNotContain(errosComSeteCartas.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_COPIAS));
            Assert.Contains(errosComMaisDeSeteCartas.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_COPIAS));
        }



        [Theory]
        [InlineData(IdCartasEspeciais.APOSTOLO_DAS_SOMBRAS)]
        [InlineData(IdCartasEspeciais.APROXIMACAO_DO_DRAGAO)]
        [InlineData(IdCartasEspeciais.COLONIA_DE_RATOS)]
        [InlineData(IdCartasEspeciais.PETICIONARIOS_PERSISTENTES)]
        [InlineData(IdCartasEspeciais.RATOS_IMPLACAVEIS)]
        public void ValidaCarta_CartaQuePermiteQuantidadeIlimitadaDeCopias_NaoRetornaErroDeLimiteDeCopias(string idCarta)
        {
            // Arrange
            var carta = new Carta
            {
                IdScryfall = idCarta
            };

            var tipoFormato = TipoFormato.Factory(new Faker().FormatoJogo());

            // Act
            var erros = tipoFormato.ValidaCarta(carta, 100);

            // Assert
            Assert.DoesNotContain(erros.Erros, e => e.Contains(MensagemDeErro.ACIMA_DO_LIMITE_DE_COPIAS));
        }


        #endregion
    }
}
