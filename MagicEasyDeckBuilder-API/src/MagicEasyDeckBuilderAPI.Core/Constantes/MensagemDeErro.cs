using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Core.Constantes
{
    public static class MensagemDeErro
    {
        public const string ABAIXO_DO_LIMITE_DE_CARTAS = "Deck com menos cartas que o limite mínimo de cartas no deck";
        public const string ACIMA_DO_LIMITE_DE_CARTAS = "Deck com mais cartas que o limite máximo de cartas no deck";
        public const string SIDE_DECK_ACIMA_DO_LIMITE_DE_CARTAS = "Side Deck com mais cartas que o limite máximo de cartas";
        public const string ACIMA_DO_LIMITE_DE_COPIAS = "Número de cópias de uma mesma carta excedido";
        public const string CARTA_NAO_LEGAL_NO_FORMATO = "Carta não é legal neste formato.";
        public const string CARTA_RESTRITA_NO_FORMATO = "Carta restrita neste formato.Pode haver apenas uma cópia desta carta no deck.";
        public const string CARTA_BANIDA_NO_FORMATO = "Carta banido neste formato.";
        public const string COMANDANTE_NAO_DEFINIDO = "Comandante não definido para o Deck.";
        public const string FORMATO_NAO_PERMITE_COMANDANTE = "Este Formato de jogo não utiliza Comandante.";
        public const string COMANDANTE_INVALIDO_BRAWL = "Apenas Criaturas Lendárias e Planeswalkers Lendários podem ser Comandante.";
        public const string COMANDANTE_INVALIDO_COMMANDER = "Apenas Criaturas Lendárias podem ser Comandante, ou e Planeswalkers Lendários hablitados para serem Comandante.";
        public const string CARTA_DIFERENTE_COR_COMANDANTE = "Carta possui Cores diferente da identidade de Cores do Comandante.";
    }
}
