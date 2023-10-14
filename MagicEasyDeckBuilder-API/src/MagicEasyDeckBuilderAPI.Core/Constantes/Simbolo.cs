﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Core.Constantes
{
    public class Simbolo
    {
        public const string MANA_BRANCO = "W";
        public const string MANA_AZUL = "U";
        public const string MANA_PRETO = "B";
        public const string MANA_VERMELHO = "R";
        public const string MANA_VERDE = "G";
        public const string MANA_INCOLOR = "C";
        public const string MANA_NEVADO = "S";

        public static IEnumerable<string> Cores = new[] {
            MANA_BRANCO,
            MANA_AZUL,
            MANA_PRETO,
            MANA_VERMELHO,
            MANA_VERDE,
            MANA_INCOLOR
        };
    }
}
