using Bogus;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Test.DadosFake
{
    public class CartaFake
    {
        public static IEnumerable<CartaDeck> GetCartasDekcGenerico(int quantidadeCartas,TipoDeckCarta tipoDeck, string tipo = null)
        {
            if (quantidadeCartas > 0)
            {
                var carta = new Carta()
                {
                    Tipo = tipo
                };
                var deck = new Deck();
                var cartaDeckFake = new Faker<CartaDeck>();
                var cartasRestantes = quantidadeCartas;

                cartaDeckFake.RuleFor(c => c.Deck, () => deck);
                cartaDeckFake.RuleFor(c => c.Carta, () => carta);
                cartaDeckFake.RuleFor(c => c.TipoDeck, () => tipoDeck);
                cartaDeckFake.RuleFor(c => c.Quantidade, (f,c) =>
                {
                    var quantidade = 4;
                    if (quantidade > cartasRestantes)
                        quantidade = cartasRestantes;

                    cartasRestantes -= quantidade;
                    return quantidade;
                });

                var cartasDeck = new List<CartaDeck>();
                while (cartasRestantes > 0)
                {
                    cartasDeck.Add(cartaDeckFake.Generate());
                }
                return cartasDeck;
            }

            return new List<CartaDeck>();
        }
    }
}
