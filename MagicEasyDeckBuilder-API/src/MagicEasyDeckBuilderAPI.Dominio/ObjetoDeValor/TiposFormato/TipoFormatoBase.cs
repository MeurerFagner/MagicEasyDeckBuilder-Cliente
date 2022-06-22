using MagicEasyDeckBuilderAPI.Core;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato
{
    public abstract class TipoFormatoBase
    {
        public string Nome { get; private set; }
        protected TipoFormatoBase(string nome)
        {
            Nome = nome;
        }

        protected virtual int GetLimiteMinimoDeCartas() => 60;
        protected virtual int GetLimiteDeCopias() => 4;
        protected virtual int GetQuantidadeSideDeck() => 15;

        public virtual RetornoValidacao ValidaDeck(Deck deck)
        {
            var retornoValidacao = new RetornoValidacao();

            ValidaQuantidadeMinimaDeCartas(deck, retornoValidacao);
            ValidaQuantidadeMaximaDeCartasDoSideDeck(deck, retornoValidacao);
            ValidaCartasDoDeck(deck, retornoValidacao);

            return retornoValidacao;
        }

        private void ValidaCartasDoDeck(Deck deck, RetornoValidacao retornoValidacao)
        {
            var deckESideCombinado = deck.MainDeck.Concat(deck.SideDeck);

            var deckGroup = deckESideCombinado.GroupBy(c => c.IdCarta)
                .Select(s => new { s.FirstOrDefault(c => c.IdCarta == s.Key).Carta, Quantidade = s.Sum(ss => ss.Quantidade) });

            foreach (var carta in deckGroup)
            {
                var validacaoCarta = ValidaCarta(carta.Carta, carta.Quantidade);
                foreach (var erro in validacaoCarta.Erros)
                {
                    retornoValidacao.AdicionarErro($"{carta.Carta.Nome}: {erro}");
                }
            }
        }

        private void ValidaQuantidadeMaximaDeCartasDoSideDeck(Deck deck, RetornoValidacao retornoValidacao)
        {
            if (deck.GetQuantidadeCartasSide() > GetQuantidadeSideDeck())
                retornoValidacao.AdicionarErro($"{MensagemDeErro.SIDE_DECK_ACIMA_DO_LIMITE_DE_CARTAS} - {deck.GetQuantidadeCartasSide()}/{GetQuantidadeSideDeck()}");
        }

        private void ValidaQuantidadeMinimaDeCartas(Deck deck, RetornoValidacao retornoValidacao)
        {
            if (deck.GetQuantidadeDeCartas() < GetLimiteMinimoDeCartas())
                retornoValidacao.AdicionarErro($"{MensagemDeErro.ABAIXO_DO_LIMITE_DE_CARTAS} - {deck.GetQuantidadeDeCartas()}/{GetLimiteMinimoDeCartas()}");
        }

        public virtual RetornoValidacao ValidaCarta(Carta carta, int quantidade)
        {
            var retornoValidacao = new RetornoValidacao();

            ValidaLimiteDeCopias(carta, quantidade, retornoValidacao);
            ValidaLegalidadeCartaPorFormato(carta, quantidade, retornoValidacao);

            return retornoValidacao;
        }

        public void ValidaLimiteDeCopias(Carta carta, int quantidade, RetornoValidacao retornoValidacao)
        {
            if (carta.GetSupertipos().Contains(SuperTipo.BASICO))
                return;

            if (CartaSemlimiteDeCopias(carta.IdScryfall))
                return;

            if (carta.IdScryfall == IdCartasEspeciais.SETE_ANOES && quantidade <= 7)
                return;

            if (quantidade > GetLimiteDeCopias())
                retornoValidacao.AdicionarErro($"{MensagemDeErro.ACIMA_DO_LIMITE_DE_COPIAS} - {quantidade}/{GetLimiteDeCopias()}");
        }

        private bool CartaSemlimiteDeCopias(string idScryfall)
        {
            return idScryfall == IdCartasEspeciais.APOSTOLO_DAS_SOMBRAS
                || idScryfall == IdCartasEspeciais.APROXIMACAO_DO_DRAGAO
                || idScryfall == IdCartasEspeciais.COLONIA_DE_RATOS
                || idScryfall == IdCartasEspeciais.PETICIONARIOS_PERSISTENTES
                || idScryfall == IdCartasEspeciais.RATOS_IMPLACAVEIS;
        }

        private void ValidaLegalidadeCartaPorFormato(Carta carta, int quantidade, RetornoValidacao retornoValidacao)
        {
            if (carta.LegalidadePorFormato.TryGetValue(Nome, out var legalidade))
            {
                if (Legalidade.NAO_LEGAL == legalidade)
                    retornoValidacao.AdicionarErro(MensagemDeErro.CARTA_NAO_LEGAL_NO_FORMATO);
                if (Legalidade.BANIDO == legalidade)
                    retornoValidacao.AdicionarErro(MensagemDeErro.CARTA_BANIDA_NO_FORMATO);
                if (Legalidade.RESTRITA == legalidade && quantidade > 1)
                    retornoValidacao.AdicionarErro(MensagemDeErro.CARTA_RESTRITA_NO_FORMATO);
            }
        }

        public static TipoFormatoBase Factory(string nome)
        {
            return nome switch
            {
                TiposFormatoJogo.CASUAL => new Casual(nome),
                TiposFormatoJogo.LEGACY => new Legacy(nome),
                TiposFormatoJogo.VINTAGE => new Vintage(nome),
                TiposFormatoJogo.MODERN => new Modern(nome),
                TiposFormatoJogo.PIONEER => new Pioneer(nome),
                TiposFormatoJogo.PAUPER => new Pauper(nome),
                TiposFormatoJogo.COMMANDER => new Commander(nome),
                TiposFormatoJogo.COMMANDER_LIVRE => new CommanderLivre(nome),
                TiposFormatoJogo.BRAWL => new Brawl(nome),
                TiposFormatoJogo.BRAWL_LIVRE => new BrawlLivre(nome),
                _ => throw new ArgumentException($"Formato '{nome}' não é válido."),
            };
        }


    }
}
