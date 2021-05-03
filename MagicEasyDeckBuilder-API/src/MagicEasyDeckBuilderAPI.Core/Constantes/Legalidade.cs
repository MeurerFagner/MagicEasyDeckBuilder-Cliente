using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Core.Constantes
{
    public class Legalidade : StringEnumeration
    {
        public static Legalidade NAO_LEGAL = new Legalidade("not_legal");
        public static Legalidade RESTRITA = new Legalidade("restricted");
        public static Legalidade BANIDO = new Legalidade("banned");
        public static Legalidade LEGAL = new Legalidade("legal");

        protected Legalidade(string valor) : base(valor)
        {
        }

        public static Legalidade Factory(string valor)
        {
            return valor switch
            {
                "not_legal" => NAO_LEGAL,
                "restricted" => RESTRITA,
                "banned" => BANIDO,
                "legal" => LEGAL,
                _ => throw new ArgumentException("Tipo de Legalidade inválida.")
            };
        }
    }
}
