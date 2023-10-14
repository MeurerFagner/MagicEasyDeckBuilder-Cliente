using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Core.Constantes
{
    public static class Raridade 
    {
        public const string COMUM = "common";
        public const string INCOMUM = "uncommon";
        public const string RARA = "rare";
        public const string MITICA = "mythic";

        public static Dictionary<string, string> Raridades => new Dictionary<string, string>
        {
            {COMUM, "Common" }, {INCOMUM,  "Uncommon"}, { RARA,"Rare"}, {MITICA," Mythic Rare   "}
        };
    }

}
