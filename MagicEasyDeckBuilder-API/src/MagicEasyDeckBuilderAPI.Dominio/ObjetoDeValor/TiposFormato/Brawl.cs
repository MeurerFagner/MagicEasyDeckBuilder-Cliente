using MagicEasyDeckBuilderAPI.Core;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato
{
    public class Brawl : TipoFormatoBase
    {
        public Brawl(string nome) : base(nome)
        {
        }
        protected override int GetLimiteDeCopias() => 1;
        protected override int GetQuantidadeSideDeck() => 0;

        public override RetornoValidacao ValidaDeck(Deck deck)
        {
            var retornoValidacao = base.ValidaDeck(deck);

            ValidaSeComandanteFoiInformado(deck, retornoValidacao);
            ValidaQuantidadeMaximaDeCartas(deck, retornoValidacao);

            if (DeckPossuiComandante(deck))
            {
                var identidadeDeCoresComandante = deck.MainDeck
                    .Where(c => c.Comandante)
                    .SelectMany(c => c.Carta.IdentidadeDeCor)
                    .Distinct();

                foreach (var carta in deck.MainDeck.Select(c => c.Carta))
                {
                    foreach (var cor in carta.IdentidadeDeCor)
                    {
                        if (!identidadeDeCoresComandante.Contains(cor))
                        {
                            retornoValidacao.AdicionarErro($"{carta.Nome}: {MensagemDeErro.CARTA_DIFERENTE_COR_COMANDANTE}");
                            break;
                        }
                    }
                }
            }

            return retornoValidacao;
        }

        private static void ValidaSeComandanteFoiInformado(Deck deck, RetornoValidacao retornoValidacao)
        {
            if (!DeckPossuiComandante(deck))
                retornoValidacao.AdicionarErro(MensagemDeErro.COMANDANTE_NAO_DEFINIDO);
        }

        private static bool DeckPossuiComandante(Deck deck)
        {
            return deck.MainDeck.Any(c => c.Comandante);
        }

        private void ValidaQuantidadeMaximaDeCartas(Deck deck, RetornoValidacao retornoValidacao)
        {
            if (deck.GetQuantidadeDeCartas() > GetLimiteMinimoDeCartas())
                retornoValidacao.AdicionarErro($"{MensagemDeErro.ACIMA_DO_LIMITE_DE_CARTAS} - {deck.GetQuantidadeDeCartas()}/{GetLimiteMinimoDeCartas()}");
        }
    }
}
