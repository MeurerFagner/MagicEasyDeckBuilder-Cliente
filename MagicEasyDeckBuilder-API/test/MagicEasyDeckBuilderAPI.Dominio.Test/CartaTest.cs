using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MagicEasyDeckBuilderAPI.Dominio.Test
{
    public class CartaTest
    {

        [Theory]
        [InlineData("Terreno Lendário")]
        [InlineData("Planeswalker Lendário — Ashiok")]
        [InlineData("Criatura Lendária — Humano Artesão")]
        [InlineData("Feitiço Lendário")]
        [InlineData("Artefato Lendário — Equipamento")]
        public void GetSupertipo_RecebeCartasLendarias_RetornaListComOSupeLendario(string tipoPrintado)
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = tipoPrintado
            };
            // Act
            var supertipos = carta.GetSupertipos();
            // Assert
            Assert.Contains(supertipos, s => s.Equals(Supertipo.LENDARIA));
        }


        [Fact]
        public void GetSupertipo_TipoTerrenoBasico_RetornaListComSuperBasico()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Terreno Básico — Montanha"
            };
            // Act
            var supertipos = carta.GetSupertipos();
            // Assert
            Assert.Contains(supertipos, s => s.Equals(Supertipo.BASICO));
        }

        [Theory]
        [InlineData("Criatura da Neve — Ent")]
        [InlineData("Terreno da Neve")]
        [InlineData("Artefato da Neve")]
        [InlineData("Feitiço da Neve")]
        [InlineData("Mágica instantânea da Neve")]
        public void GetSupertipo_RecebeCartasNevadas_RetornaListComOSupeNevado(string tipoPrintado)
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = tipoPrintado
            };
            // Act
            var supertipos = carta.GetSupertipos();
            // Assert
            Assert.Contains(supertipos, s => s.Equals(Supertipo.NEVADO));
        }

    }
}
