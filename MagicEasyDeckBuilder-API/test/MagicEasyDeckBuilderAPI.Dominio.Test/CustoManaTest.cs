using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using Xunit;

namespace MagicEasyDeckBuilderAPI.Dominio.Test
{
    public class CustoManaTest
    {
        [Theory]
        [InlineData("W")]
        [InlineData("U")]
        [InlineData("B")]
        [InlineData("R")]
        [InlineData("G")]
        [InlineData("C")]
        [InlineData("S")]
        public void CustoDeManaConvertido_RecebeUmManaBasico_RetornaCmc1(string mana)
        {
            var custoMana = new CustoMana($"{{{mana}}}");

            Assert.Equal(1, custoMana.CustoDeManaConvertido);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void CustoDeManaConvertido_RecebeManaGenerico_RetornaOValorDaManaGenerica(int valor)
        {
            var custoMana = new CustoMana($"{{{valor}}}");

            Assert.Equal(valor, custoMana.CustoDeManaConvertido);
        }


        [Theory]
        [InlineData("W/P")]
        [InlineData("U/P")]
        [InlineData("B/P")]
        [InlineData("R/P")]
        [InlineData("G/P")]
        [InlineData("P")]
        public void CustoDeManaConvertido_RecebeMana_RetornoCmc1(string mana)
        {
            var custoMana = new CustoMana($"{{{mana}}}");

            Assert.Equal(1, custoMana.CustoDeManaConvertido);
        }

        [Theory]
        [InlineData("W/B",1)]
        [InlineData("2/U",2)]
        [InlineData("4/B",4)]
        public void CustoDeManaConvertido_RecebeManaHibrido_RetornoMaiorValorDeCusto(string mana,int cmcEsperado)
        {
            var custoMana = new CustoMana($"{{{mana}}}");

            Assert.Equal(cmcEsperado, custoMana.CustoDeManaConvertido);
        }

        [Theory]
        [InlineData("X")]
        [InlineData("Y")]
        [InlineData("Z")]
        public void CustoDeManaConvertido_RecebeManaCustoVariavelDeManaGenerica_Retorna0(string mana)
        {
            var custoMana = new CustoMana($"{{{mana}}}");

            Assert.Equal(0, custoMana.CustoDeManaConvertido);
        }


        [Theory]
        [InlineData("{6}{B}", 7)]
        [InlineData("{2/B}{2/U}", 4)]
        [InlineData("{3}{G/P}", 4)]
        [InlineData("{X}{U}", 1)]
        [InlineData("{X}{Y}{Z}", 0)]
        public void CustoDeManaConvertido_RecebeCustosDeManasCombinados_RetornoOcmcEsperado(string mana, int cmcEsperado)
        {
            var custoMana = new CustoMana($"{{{mana}}}");

            Assert.Equal(cmcEsperado, custoMana.CustoDeManaConvertido);
        }


    }
}
