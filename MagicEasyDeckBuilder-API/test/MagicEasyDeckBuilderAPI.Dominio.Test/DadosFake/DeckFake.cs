using Bogus;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Test.DadosFake
{
    public class DeckFake
    {
         public static Deck GetDeckPorformato(string tipoformato, int quantidadeCartas, int quantidadeSideDeck = 0)
        {
            var deckFake = new Faker<Deck>();
            var main = CartaFake.GetCartasDekcGenerico(quantidadeCartas,TipoDeckCarta.MainDeck);
            var side = CartaFake.GetCartasDekcGenerico(quantidadeSideDeck,TipoDeckCarta.SideDeck);

            var cartas = main.Concat(side);

            deckFake.RuleFor(d => d.TipoFormato, (f, c) => TipoFormatoBase.Factory(tipoformato));
            deckFake.RuleFor(d => d.Cartas, (f, c) => cartas.ToList());

            return deckFake.Generate();
        }
    }
}
