using MagicEasyDeckBuilderAPI.Core;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato
{
    public class Brawl : TipoFormato
    {
        public Brawl(string nome) : base(nome)
        {
        }
        protected override int GetLimiteDeCopias() => 1;
        protected override int GetQuantidadeSideDeck() => 0;

        public override RetornoValidacao ValidaDeck(Deck deck)
        {
            var retornoValidacao = base.ValidaDeck(deck);

            ValidaQuantidadeMaximaDeCartas(deck, retornoValidacao);

            return retornoValidacao;
        }

        private void ValidaQuantidadeMaximaDeCartas(Deck deck, RetornoValidacao retornoValidacao)
        {
            if (deck.GetQuantidadeDeCartas() > GetLimiteMinimoDeCartas())
                retornoValidacao.AdicionarErro($"{MensagemDeErro.ACIMA_DO_LIMITE_DE_CARTAS} - {deck.GetQuantidadeDeCartas()}/{GetLimiteMinimoDeCartas()}");
        }
    }
}
