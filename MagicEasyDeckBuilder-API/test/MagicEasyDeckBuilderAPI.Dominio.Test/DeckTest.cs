using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MagicEasyDeckBuilderAPI.Dominio.Test
{
    public class DeckTest
    {

        [Fact]
        public void AdicionarComandante_DadoQueODeckEstaSemComandante_DeveIncluirUmNovoComandante()
        {
            // Arrange
            var deck = new Deck();
            var carta = new Carta();
            // Act
            deck.AdicionarComandante(carta);
            // Assert
            Assert.Contains(deck.MainDeck, c => c.Comandante);
        }


        [Fact]
        public void AdicionarComandante_DeckJaPossuiComandante_RetornaUmaExcessao()
        {
            // Arrange
            var deck = new Deck();
            deck.AdicionarComandante(new Carta());
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => deck.AdicionarComandante(new Carta()));
        }

        [Fact]
        public void AdicionarComandante_DeckJaPossuiComandanteComUmParceiro_AdicionaParceiroComoComandante()
        {
            // Arrange
            var deck = new Deck();
            var carta1 = new Carta
            {
                NomeOriginal = "Parceiro 1",
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO
                }
            };
            var carta2 = new Carta
            {
                NomeOriginal = "Parceiro 2",
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO
                }
            };
            // Act 
            deck.AdicionarComandante(carta1);
            deck.AdicionarComandante(carta2);
            // Assert
            Assert.True(deck.MainDeck.Count(c => c.Comandante) == 2);
        }

        //TODO: Comandante Amigos ParaSempre
        [Fact]
        public void AdicionarComandante_DeckJaPossuiComandanteComAmigosParaSempre_AdicionaAmigoComoComandante()
        {
            // Arrange
            //var deck = new Deck();
            //var carta1 = new Carta
            //{
            //    NomeOriginal = "Parceiro 1",
            //    Keywords = new List<string>
            //    {
            //        Keywords.PARCEIRO
            //    }
            //};
            //var carta2 = new Carta
            //{
            //    NomeOriginal = "Parceiro 2",
            //    Keywords = new List<string>
            //    {
            //        Keywords.PARCEIRO
            //    }
            //};
            //// Act 
            //deck.AdicionarComandante(carta1);
            //deck.AdicionarComandante(carta2);
            //// Assert
            //Assert.True(deck.MainDeck.Count(c => c.Comandante) == 2);
        }

        //TODO: Comandantes Com Antecedente
        [Fact]
        public void AdicionarComandante_DeckJaPossuiComandanteComPossuiAntecedente_AdicionaAntecedenteComoComandante()
        {
            //// Arrange
            //var deck = new Deck();
            //var carta1 = new Carta
            //{
            //    NomeOriginal = "Parceiro 1",
            //    Keywords = new List<string>
            //    {
            //        Keywords.PARCEIRO
            //    }
            //};
            //var carta2 = new Carta
            //{
            //    NomeOriginal = "Parceiro 2",
            //    Keywords = new List<string>
            //    {
            //        Keywords.PARCEIRO
            //    }
            //};
            //// Act 
            //deck.AdicionarComandante(carta1);
            //deck.AdicionarComandante(carta2);
            //// Assert
            //Assert.True(deck.MainDeck.Count(c => c.Comandante) == 2);
        }

        [Fact]
        public void AdicionarComandante_DeckJaPossuiComandanteComParceiroMasRecebeOutroComandanteNaoParceiro_RetornaUmaExcecao()
        {
            // Arrange
            var deck = new Deck();
            var cartaComandante = new Carta
            {
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO
                }
            };

            // Act 
            deck.AdicionarComandante(cartaComandante);

            // Assert
            Assert.Throws<InvalidOperationException>(() => deck.AdicionarComandante(new Carta()));
        }

        [Fact]
        public void AdicionarComandante_DeckJaPossuiDoisComandanteComParceiro_RetornarUmaExcesso()
        {
            // Arrange
            var deck = new Deck();
            var carta1 = new Carta
            {
                NomeOriginal = "Parceiro 1",
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO
                }
            };
            var carta2 = new Carta
            {
                NomeOriginal = "Parceiro 2",
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO
                }
            };
            var carta3 = new Carta
            {
                NomeOriginal = "Parceiro 3",  
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO
                }
            };
            // Act 
            deck.AdicionarComandante(carta1);
            deck.AdicionarComandante(carta2);
            // Assert
            Assert.Throws<InvalidOperationException>(() => deck.AdicionarComandante(carta3));
        }

        [Fact]
        public void AdicionarComandante_DeckPossuiComandanteComParceiroEspecificoERecebeParceiroDiferenteDoDefinidoParaCarta_RetornaumExcecao()
        {
            // Arrange
            var deck = new Deck();
            var cartaUkkima = new Carta
            {
                Nome = "Ukkima, Sombra Espreitadora",
                NomeOriginal = "Ukkima, Stalking Shadow",
                TextoOriginal = "Partner with Cazur, Ruthless Stalker (When this creature enters the battlefield, target player may put Cazur into their hand from their library, then shuffle.)\nUkkima, Stalking Shadow can't be blocked.\nWhen Ukkima leaves the battlefield, it deals X damage to target player and you gain X life, where X is its power.",
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO,
                    Keywords.PARCEIRO_COM
                }
            };

            var carta = new Carta
            {
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO
                }
            };

            // Act
            deck.AdicionarComandante(cartaUkkima);
            // Assert
            Assert.Throws<InvalidOperationException>(() => deck.AdicionarComandante(carta));
        }

        [Theory]
        [InlineData("Rowan Kenrith",  
                    " + 2: During target player's next turn, each creature that player controls attacks if able.\n−2: Rowan Kenrith deals 3 damage to each tapped creature target player controls.\n−8: Target player gets an emblem with \"Whenever you activate an ability that isn't a mana ability, copy it.You may choose new targets for the copy.\"\nPartner with Will Kenrith\nRowan Kenrith can be your commander.",
                    "Will Kenrith")]
        [InlineData("Ukkima, Stalking Shadow",
                    "Partner with Cazur, Ruthless Stalker (When this creature enters the battlefield, target player may put Cazur into their hand from their library, then shuffle.)\nUkkima, Stalking Shadow can't be blocked.\nWhen Ukkima leaves the battlefield, it deals X damage to target player and you gain X life, where X is its power.",
                    "Cazur, Ruthless Stalker")]
        public void AdicionarComandante_DeckPossuiComandanteComParceiroEspecificoERecebeParceiroDefinidoParaCarta_AdicionaParceiroComoComandante(string nomeComandanteAtual, string textoOriginal, string nomeParceiroComandante)
        {
            // Arrange
            var deck = new Deck();
            var comandanteAtual = new Carta
            {
                NomeOriginal = nomeComandanteAtual,
                TextoOriginal = textoOriginal,
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO,
                    Keywords.PARCEIRO_COM
                }
            };
            var comandanteParceiro = new Carta
            {
                NomeOriginal = nomeParceiroComandante,
                Keywords = new List<string>
                {
                    Keywords.PARCEIRO,
                    Keywords.PARCEIRO_COM
                }
            };

            // Act
            deck.AdicionarComandante(comandanteAtual);
            deck.AdicionarComandante(comandanteParceiro);

            // Assert
            Assert.True(deck.MainDeck.Count(c => c.Comandante) == 2);
        }


    }
}
