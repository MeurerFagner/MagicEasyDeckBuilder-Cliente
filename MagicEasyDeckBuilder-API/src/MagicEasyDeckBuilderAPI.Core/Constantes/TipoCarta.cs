using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Core.Constantes
{
    public static class TipoCarta
    {
        public const string ARTEFATO = "Artifact";
        public const string CRIATURA = "Creature";
        public const string ENCANTAMENTO = "Enchantment";
        public const string MAGICA_INSTANTEANEA = "Instant";
        public const string TERRENO = "Land";
        public const string PLANESWALKER = "Planeswalker";
        public const string FEITICO = "Sorcery";
        public const string MAGIA_E_FEITICO = "Spell";
        public const string TRIBAl = "Tribal";

        public static IEnumerable<string> Tipos = new List<string>
        {
            ARTEFATO,
            CRIATURA, 
            ENCANTAMENTO, 
            MAGICA_INSTANTEANEA, 
            TERRENO, PLANESWALKER, 
            FEITICO, 
            TRIBAl
        };

        public const string LENDARIA = "Legendary";
        public const string BASICO = "Basic";
        public const string NEVADO = "Snow";
        public const string MUNDO = "World";

        public static IEnumerable<string> SuperTipos = new List<string>
        {
            LENDARIA, BASICO, NEVADO, MUNDO
        };
    }
}
