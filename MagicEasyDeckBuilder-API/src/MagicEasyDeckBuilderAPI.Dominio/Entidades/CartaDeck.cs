using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class CartaDeck
    {
        public Guid Id { get; set; }
        public Guid IdDeck { get; set; }
        public Deck Deck { get; set; }
        public Guid IdCarta { get; set; }
        public Carta Carta { get; set; }
        public int Quantidade { get; set; }
    }
}
