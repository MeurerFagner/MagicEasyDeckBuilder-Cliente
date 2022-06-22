using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MagicEasyDeckBuilderAPI.Dominio.Test
{
    public class CartaTipoTest
    {
        //SUPERTIPOS---------
        [Theory]
        [InlineData("Legendary Land")]
        [InlineData("Legendary Planeswalker — Ashiok")]
        [InlineData("Legendary Creature — Humano Artesão")]
        [InlineData("Legendary Sorcery")]
        [InlineData("Legendary Snow Land")]
        public void GetSupertipos_RecebeCartasLendarias_RetornaListComOSupeLendario(string tipoPrintado)
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = tipoPrintado
            };
            // Act
            var supertipos = carta.GetSupertipos();
            // Assert
            Assert.Contains(supertipos, s => s.Equals(SuperTipo.LENDARIA));
        }


        [Fact]
        public void GetSupertipos_TipoTerrenoBasico_RetornaListComSuperBasico()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Basic Land — Island"
            };
            // Act
            var supertipos = carta.GetSupertipos();
            // Assert
            Assert.Contains(supertipos, s => s.Equals(SuperTipo.BASICO));
        }

        [Theory]
        [InlineData("Snow Criature — Ent")]
        [InlineData("Snow Land")]
        [InlineData("Snow Artifact")]
        [InlineData("Snow Sorcery")]
        [InlineData("Snow Instant")]
        public void GetSupertipos_RecebeCartasNevadas_RetornaListComOSupeNevado(string tipoPrintado)
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = tipoPrintado
            };
            // Act
            var supertipos = carta.GetSupertipos();
            // Assert
            Assert.Contains(supertipos, s => s.Equals(SuperTipo.NEVADO));
        }


        // TIPOS -------------
        [Theory]
        [InlineData("Instant")]
        [InlineData("Instant - Arcana")]
        public void GetTipos_RecebeUmaMagicaInstantanea_RetornaOTipoMagicaInstantanea(string tipo)
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = tipo
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 1);
            Assert.True(tipos.First() == TipoCarta.MAGICA_INSTANTEANEA);
        }

        [Theory]
        [InlineData("Sorcery")]
        [InlineData("Snow Sorcery")]
        [InlineData("Sorcery - Arcane")]
        public void GetTipos_RecebeUmFeitico_RetornaOTipoFeitico(string tipo)
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = tipo
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 1);
            Assert.True(tipos.First() == TipoCarta.FEITICO);
        }

        [Fact]
        public void GetTipos_RecebeUmTerreno_RetornaOTipoTerreno()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Legendary Land"
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 1);
            Assert.True(tipos.First() == TipoCarta.TERRENO);
        }

        [Fact]
        public void GetTipos_RecebeUmaCriatura_RetornaOTipoCriatua()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Legendary Creature — Elf Horror"
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 1);
            Assert.True(tipos.First() == TipoCarta.CRIATURA);
        }
        [Fact]
        public void GetTipos_RecebeUmArtefato_RetornaOTipoArtefato()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Legendary Artifact"
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 1);
            Assert.True(tipos.First() == TipoCarta.ARTEFATO);
        }
        [Fact]
        public void GetTipos_RecebeUmEncantamento_RetornaOTipoEncantamento()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Legendary Enchantment"
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 1);
            Assert.True(tipos.First() == TipoCarta.ENCANTAMENTO);
        }

        [Fact]
        public void GetTipos_RecebeUmPlaneswalker_RetornaOTipoplaneswalker()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Legendary Planeswalker — Saheeli"
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 1);
            Assert.True(tipos.First() == TipoCarta.PLANESWALKER);
        }

        [Fact]
        public void GetTipos_RecebeMaisDeUmTipo_RetornaTodosOsTipos()
        {
            var carta = new Carta
            {
                Tipo = "Legendary Artifact Creature - Golem"
            };
            // Act
            var tipos = carta.GetTipos();
            // Assert
            Assert.True(tipos.Count() == 2);
            Assert.Contains(tipos,t => t == TipoCarta.CRIATURA);
            Assert.Contains(tipos,t => t == TipoCarta.ARTEFATO);
        }


        [Fact]
        public void GetSubtipo_RecebeCartaComVariosSubtipos_RetornaTodoOsTiposInformdos()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Creature - Humano Soldado Aliado"
            };
            // Act
            var subtipos = carta.GetSubtipos();

            // Assert
            Assert.True(subtipos.Count() == 3);
            Assert.Contains(subtipos, t => t == "Humano");
            Assert.Contains(subtipos, t => t == "Soldado");
            Assert.Contains(subtipos, t => t == "Aliado");
        }


        [Fact]
        public void GetSubtipo_RecebeCartaSemSubtipo_RetornaUmaListaVazia()
        {
            // Arrange
            var carta = new Carta
            {
                Tipo = "Sorcery"
            };
            // Act
            var subtipos = carta.GetSubtipos();

            // Assert
            Assert.NotNull(subtipos);
            Assert.Empty(subtipos);
        }



    }
}
