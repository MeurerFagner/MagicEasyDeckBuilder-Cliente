using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class Deck
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public string Nome { get; set; }
        public TipoFormato  TipoFormato { get; set; }
        public virtual ICollection<CartaDeck> Cartas { get; set; }
        public virtual ICollection<CartaDeck> SideDeck { get; set; }

        public Deck()
        {
            Cartas = new List<CartaDeck>();
            SideDeck = new List<CartaDeck>();
        }

        public int GetQuantidadeDeCartas()
        {
            return Cartas.Sum(c => c.Quantidade);
        }

        public int GetQuantidadeCartasSide()
        {
            return SideDeck.Sum(s => s.Quantidade);
        }
    }
}
