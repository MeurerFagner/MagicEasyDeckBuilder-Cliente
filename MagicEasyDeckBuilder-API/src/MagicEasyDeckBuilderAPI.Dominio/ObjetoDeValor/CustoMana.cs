using MagicEasyDeckBuilderAPI.Core.Constantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor
{
    public class CustoMana
    {
        public string Custo { get; private set; }

        public int CustoDeManaConvertido
        {
            get
            {
                var custos = Custo.Replace("{", "").Split("}");
                var cmc = 0;
                foreach (var custoItem in custos)
                {
                    string custo = SubstituiManaPhyrexianoPorumManaGenerico(custoItem);
                    custo = SubstituiManaHibridoPorManaGenerico(custo);

                    cmc += ObtemValoDoSimboloCustoDeMana(custo);

                }
                return cmc;
            }
        }
        public CustoMana(string custo)
        {
            Custo = custo;
        }

        private string SubstituiManaHibridoPorManaGenerico(string custo)
        {
            var custoHibrido = custo.Split('/');

            if (custoHibrido.Length > 1)
            {
                var custo1 = ObtemValoDoSimboloCustoDeMana(custoHibrido[0]);
                var custo2 = ObtemValoDoSimboloCustoDeMana(custoHibrido[1]);

                custo = custo1 > custo2 ? custo1.ToString() : custo2.ToString();
            }

            return custo;
        }

        private static string SubstituiManaPhyrexianoPorumManaGenerico(string custoItem)
        {
            return custoItem.ToUpper().Replace("P", "1");
        }

        private int ObtemValoDoSimboloCustoDeMana(string custo)
        {
            var cmc = 0;
            if (GetManaBasicos().Contains(custo))
                cmc += 1;
            else if (Int32.TryParse(custo, out var valorMana))
                cmc += valorMana;
            return cmc;
        }

        private List<string> GetManaBasicos()
        {
            return new List<string>
            {
                Simbolo.MANA_BRANCO,
                Simbolo.MANA_AZUL,
                Simbolo.MANA_PRETO,
                Simbolo.MANA_VERMELHO,
                Simbolo.MANA_VERDE,
                Simbolo.MANA_INCOLOR,
                Simbolo.MANA_NEVADO
            };
        }
    }
}
