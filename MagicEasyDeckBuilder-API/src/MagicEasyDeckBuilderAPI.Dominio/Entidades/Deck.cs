using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class Deck : EntidadeBase
    {
        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public string Nome { get; set; }
        public string Capa { get; set; }
        public TipoFormatoBase TipoFormato { get; set; }
        public IEnumerable<string> Erros { get; private set; }
        public virtual ICollection<CartaDeck> Cartas { get; set; }
        public IEnumerable<CartaDeck> MainDeck => Cartas.Where(c => c.TipoDeck == TipoDeckCarta.MainDeck);
        public IEnumerable<CartaDeck> SideDeck => Cartas.Where(c => c.TipoDeck == TipoDeckCarta.SideDeck);
        public IEnumerable<CartaDeck> MaybeDeck => Cartas.Where(c => c.TipoDeck == TipoDeckCarta.MaybeDeck);

        public Deck()
        {
            Cartas = new List<CartaDeck>();
            Erros = new List<string>();
        }

        public int GetQuantidadeDeCartas()
        {
            return MainDeck.Sum(c => c.Quantidade);
        }

        public int GetQuantidadeCartasSide()
        {
            return SideDeck.Sum(s => s.Quantidade);
        }

        public void AdicionarComandante(Carta carta)
        {
            if (DeckJaPossuiComandante() && !ComandanteParceiroDoComandanteAtual(carta))
                throw new InvalidOperationException("Deck Já possui um Comandante informado.");

            AdicionaCarta(TipoDeckCarta.MainDeck, carta, true);
        }

        public void AdicionarCartaMainDeck(Carta carta)
        {
            AdicionaCarta(TipoDeckCarta.MainDeck, carta);
        }

        public void AdicionarCartaSideDeck(Carta carta)
        {
            AdicionaCarta(TipoDeckCarta.SideDeck, carta);
        }

        public void AdicionarCartaMaybeDeck(Carta carta)
        {
            AdicionaCarta(TipoDeckCarta.MaybeDeck, carta);
        }

        private void AdicionaCarta(TipoDeckCarta tipoDeck, Carta carta, bool comandante = false)
        {
            var cartaDeck = Cartas.FirstOrDefault(
                c => c.Carta.NomeOriginal == carta.NomeOriginal
                  && c.TipoDeck == tipoDeck);

            if (cartaDeck == null)
            {
                cartaDeck = new CartaDeck
                {
                    IdDeck = Id,
                    Deck = this,
                    IdCarta = carta.Id,
                    Carta = carta,
                    Comandante = comandante,
                    TipoDeck = tipoDeck
                };
            }
            else
            {
                Cartas.Remove(cartaDeck);
            }

            cartaDeck.Quantidade++;
            Cartas.Add(cartaDeck);

            if (tipoDeck != TipoDeckCarta.MaybeDeck)
                ValidarDeck();

            if (string.IsNullOrEmpty(Capa))
                Capa = carta.UrlCropImage;
        }

        public void RemoverCartaMainDeck(Carta carta) => RemoverCarta(carta, TipoDeckCarta.MainDeck);
        public void RemoverCartaSideDeck(Carta carta) => RemoverCarta(carta, TipoDeckCarta.SideDeck);
        public void RemoverCartaMaybeDeck(Carta carta) => RemoverCarta(carta, TipoDeckCarta.MaybeDeck);

        private void RemoverCarta(Carta carta, TipoDeckCarta tipoDeck)
        {
            var cartaDeck = Cartas.FirstOrDefault(c => c.IdCarta == carta.Id && c.TipoDeck == tipoDeck);

            cartaDeck.Quantidade--;

            if (cartaDeck.Quantidade == 0)
                Cartas.Remove(cartaDeck);

            ValidarDeck();
        }

        private bool DeckJaPossuiComandante()
        {
            return MainDeck.Any(c => c.Comandante);
        }

        private bool ComandanteParceiroDoComandanteAtual(Carta carta)
        {
            if (MainDeck.Count(c => c.Comandante) >= 2) return false;

            var comandanteAtual = MainDeck.FirstOrDefault(c => c.Comandante).Carta;

            if (!CardPossuiAHabilidadeParceiro(comandanteAtual)) return false;

            if (!CardPossuiAHabilidadeParceiro(carta)) return false; ;

            if (comandanteAtual.Keywords.Contains(Keywords.PARCEIRO_COM))
            {
                string nomeParceiroDoComandante = ObtemNomeDoParceiroDaCarta(comandanteAtual);

                if (nomeParceiroDoComandante != carta.NomeOriginal) return false;
            }

            return true;
        }

        private static string ObtemNomeDoParceiroDaCarta(Carta carta)
        {
            var nomeParceiroDaCarta = Regex.Match(carta.TextoOriginal, @"(?<=Partner with)[\s\S]+?(?=[(\n])", RegexOptions.IgnoreCase)!.Value;

            return nomeParceiroDaCarta.Trim();
        }

        private bool CardPossuiAHabilidadeParceiro(Carta carta)
        {
            return carta.Keywords != null && carta.Keywords.Contains(Keywords.PARCEIRO);
        }

        public void ValidarDeck()
        {
            if (TipoFormato == null)
                return;

            var validacao = TipoFormato.ValidaDeck(this);

            Erros = validacao.Erros;

            ValidaCartas(MainDeck);
            ValidaCartas(SideDeck);
        }

        private void ValidaCartas(IEnumerable<CartaDeck> cartas)
        {
            var validacao = TipoFormato.ValidaDeck(this);

            foreach (var carta in cartas)
            {
                carta.Erros = validacao.Erros
                    .Select(s => s.Split(":"))
                    .Where(w => w.Length > 1 && w[0] == carta.Carta.Nome)
                    .Select(s => s[1])
                    .ToList();
            }
        }
    }
}
