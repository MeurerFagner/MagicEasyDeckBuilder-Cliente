using Bogus;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Test.DadosFake
{
    public class DeckFake
    {
         public static Deck GetDeckPorformato(string tipoformato, int quantidadeCartas, int quantidadeSideDeck = 0)
        {
            var deckFake = new Faker<Deck>();
            var cartas = CartaFake.GetCartasDekcGenerico(quantidadeCartas);
            var side = CartaFake.GetCartasDekcGenerico(quantidadeSideDeck);

            deckFake.RuleFor(d => d.TipoFormato, (f, c) => TipoFormato.Factory(tipoformato));
            deckFake.RuleFor(d => d.Cartas, (f, c) => cartas);
            deckFake.RuleFor(d => d.SideDeck, (f, c) => side);

            return deckFake.Generate();
        }
    }
}
