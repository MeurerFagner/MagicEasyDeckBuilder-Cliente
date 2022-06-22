using System.Collections.Generic;

namespace MagicEasyDeckBuilderAPI.Core.Constantes
{
    public static class SuperTipo
    {
        public const string LENDARIA = "Legendary";
        public const string BASICO = "Basic";
        public const string NEVADO = "Snow";
        public const string MUNDO  = "World";
        
        public static IEnumerable<string> SuperTipos = new List<string>
        {
            LENDARIA, BASICO, NEVADO, MUNDO
        };
    }
}
