using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class CartaDeck: EntidadeBase
    {
        public Guid IdDeck { get; set; }
        public Deck Deck { get; set; }
        public Guid IdCarta { get; set; }
        public TipoDeckCarta TipoDeck { get; set; }
        public Carta Carta { get; set; }
        public int Quantidade { get; set; }
        public bool Comandante { get; set; }
        public IEnumerable<string> Erros { get; set; }

        public CartaDeck()
        {
            Erros = new List<string>();
        }
    }
}
