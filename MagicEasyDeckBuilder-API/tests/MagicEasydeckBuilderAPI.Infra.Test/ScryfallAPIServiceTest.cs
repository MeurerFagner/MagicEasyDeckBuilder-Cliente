using FluentAssertions;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Infra.DadosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MagicEasydeckBuilderAPI.Infra.Test
{
    public class ScryfallAPIServiceTest
    {
        private readonly ScryfallApiService _apiService;

        public ScryfallAPIServiceTest()
        {
            _apiService = new ScryfallApiService();
        }


        [Fact]
        public async Task BuscaCarta_BuscaNaoCorrespondeNenhumaCarta_DeveRetornarNull()
        {
            // Arrange
            // Act
            var response = await _apiService.BuscaCartas("cartaQueNaoExiste");
            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task BuscaCartaPorNome_BuscaNaoCorrespondeNenhumaCarta_DeveRetornarNull()
        {
            // Arrange
            // Act
            var response = await _apiService.BucaCartaPorNome("cartaQueNaoExiste");
            // Assert
            Assert.Null(response);
        }

        [Theory]
        [InlineData("Elbrus, the Binding Blade", "Legendary Artifact — Equipment", "", "{7}", "Withengar Unbound", "Legendary Creature — Demon", "B", "")]
        [InlineData("Budoka Gardener", "Creature — Human Monk", "G", "{1}{G}", "Dokai, Weaver of Life", "Legendary Creature — Human Monk", "G", "")]
        [InlineData("Commit", "Instant", "U", "{3}{U}", "Memory", "Sorcery", "U", "{4}{U}{U}")]
        public async Task BuscaCartas_BuscaCartaDuplaFace_RetornaCartaComInformacaoDosDoisLados(string cartaNome, string tipo, string cores, string custo, 
                                                                                                string nomeOutroLado, string tipoOutroLado, string coresOutroLado, string custoOutroLado)
        {
            // Arrange
            var coresList = cores.Split(",").ToList();
            var coresOutroladoList = coresOutroLado.Split(",").ToList();

            // Act
            var response = await _apiService.BuscaCartas(nome: cartaNome);
            var carta = response?.Cartas.FirstOrDefault();

            // Assert
            carta.Faces?.Should().NotBeNull();
            carta.CardDuplo.Should().BeTrue();

            carta.Faces?.FirstOrDefault().NomeOriginal.Should().Be(cartaNome);
            carta.Faces?.FirstOrDefault().Tipo.Should().Be(tipo);
            carta.Faces?.FirstOrDefault().CustoMana.Custo.Should().Be(custo);

            carta.Faces?.LastOrDefault().NomeOriginal.Should().Be(nomeOutroLado);
            carta.Faces?.LastOrDefault().Tipo.Should().Be(tipoOutroLado);
            carta.Faces?.LastOrDefault().CustoMana.Custo.Should().Be(custoOutroLado);
        }


        [Fact]
        public async Task BuscaCartas_PassaNomeEspecificoDeUmaCarta_RetornaCaratEspecificada()
        {
            // Arrange
            var nome = "dragon";

            // Act
            var response = await _apiService.BuscaCartas(nome: nome);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
            Assert.All(cartas, c => c.NomeOriginal.ToLower().Contains(nome));
        }


        [Theory]
        [InlineData(TiposFormatoJogo.BRAWL)]
        [InlineData(TiposFormatoJogo.COMMANDER)]
        [InlineData(TiposFormatoJogo.LEGACY)]
        [InlineData(TiposFormatoJogo.PAUPER)]
        [InlineData(TiposFormatoJogo.PIONEER)]
        [InlineData(TiposFormatoJogo.MODERN)]
        [InlineData(TiposFormatoJogo.VINTAGE)]
        public async Task BuscaCartas_FiltraPorFormato_RetornaApenasCartasLegaisOuRestritasNoFormato(string formato)
        {
            // Arrange
            // Act
            var response = await _apiService.BuscaCartas(formato: formato);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
            Assert.DoesNotContain(cartas, c => c.LegalidadePorFormato[formato] != Legalidade.LEGAL && c.LegalidadePorFormato[formato] != Legalidade.RESTRITA);
        }


        [Fact]
        public async Task BuscaCartas_FiltraPorTipo_RetornaApenasCartasQuePossuaOTipo()
        {
            // Arrange
            var tipos = new List<string>
            {
                "creature",
                "dragon"
            };

            // Act
            var response = await _apiService.BuscaCartas(tipos: tipos);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
            //TODO: adiciona teste para ver se tem alguma carta que não possuia o tipo
        }


        [Theory]
        [InlineData(Raridade.COMUM)]
        [InlineData(Raridade.INCOMUM)]
        [InlineData(Raridade.RARA)]
        [InlineData(Raridade.MITICA)]
        public async Task BuscarCartas_FiltraPorRaridade_RetornaApenasCartasDaRaridade(string raridade)
        {
            // Arrange
            // Act
            var response = await _apiService.BuscaCartas(raridade: raridade);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
            Assert.DoesNotContain(cartas, c => c.Raridade != raridade);
        }


        [Fact]
        public async Task BuscarCartas_FiltraPorEdição_RetornaCartasDaEdicao()
        {
            // Arrange
            var edicao = "war"; //War of the Spark
            
            // Act
            var response = await _apiService.BuscaCartas(tipos: new List<string> { "creature","bird" }, edicao: edicao);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
            Assert.Contains(cartas, c => c.NomeOriginal == "War Screecher");
        }


        [Theory]
        [InlineData("1W")]
        [InlineData("XRG")]
        [InlineData("RXG")]
        [InlineData("0")]
        public async Task BuscarCarta_FiltroPorCustoDeMana_RetornaCartasdoCusto(string custo)
        {
            // Arrange
            // Act
            var response = await _apiService.BuscaCartas(custoMana: custo);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
        }



        [Fact]
        public async Task BuscarCartas_FiltroPorValorDeMana_RetornaCartasDoValor()
        {
            // Act
            var response = await _apiService.BuscaCartas(valorMana: 5);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
        }


        [Fact]
        public async Task BuscaCartas_FiltraPorIdentidadeDeCor_RetornaCartasNaIdentidade()
        {
            // Arrange
            var identidadeDeCor = "rg";

            // Act
            var response = await _apiService.BuscaCartas(identidadeDeCor: identidadeDeCor);
            var cartas = response?.Cartas;

            // Assert
            Assert.True(cartas.Count() > 0);
        }


        [Fact]
        public async Task BuscaCartas_FiltraPorExtamenteDasCores_RetornaCartasComAsMesmasCoresInformada()
        {
            // Arrange
            var cores = "UG";

            // Act
            var response = await _apiService.BuscaCartas(filtroDeCor: cores, tipoFiltroCor: TipoConsultaCor.EXATAMENTE_ESTAS_CORES);
            var cartas = response?.Cartas;

            // Assert
            cartas.Should().NotBeEmpty();
            cartas.Should().OnlyContain(c => c.IdentidadeDeCor.Contains("U") && c.IdentidadeDeCor.Contains("G"));
        }


        [Fact]
        public async Task BuscarCartas_FiltraQueContenhaAsCores_RetornaCartasQueContenhaAsCoresInformadas()
        {
            // Arrange
            var cores = "UG";

            // Act
            
            var response = await _apiService.BuscaCartas(filtroDeCor: cores, tipoFiltroCor: TipoConsultaCor.INCLUA_ESTAS_CORES);
            var cartas = response?.Cartas;

            // Assert
            cartas.Should().NotBeEmpty();
        }

        [Fact]
        public async Task BuscarCartas_FiltraNomAximoAquelasCores_RetornaCartasQueNaoContenhaNehumaCorAlemDaInformada()
        {
            // Arrange
            var cores = "UG";

            // Act
            var response = await _apiService.BuscaCartas(filtroDeCor: cores, tipoFiltroCor: TipoConsultaCor.NO_MAXIMO_ESTAS_CORES);
            var cartas = response?.Cartas;

            // Assert
            cartas.Should().NotBeEmpty();
        }


        [Fact]
        public async Task BuscarCartas_FiltraPorTextoNaCarta_RetornaCartasQueContenhamAqueleTexto()
        {
            // Arrange
            var texto = "{T}: Add {C}{C}.";

            // Act
            var response = await _apiService.BuscaCartas(texto: texto);
            var cartas = response?.Cartas;

            // Assert
            Assert.Contains(cartas, c => c.NomeOriginal == "Sol Ring");
        }


        [Fact]
        public async Task BuscaCartaPorNome_InformaNomeDaCarta_BuscaCartaComNomeInformado()
        {
            // Arrange
            var cardNome = "Sol Ring";

            // Act
            var carta = await _apiService.BucaCartaPorNome(cardNome);

            // Assert
            Assert.Equal(cardNome, carta.NomeOriginal);
        }

        [Fact]
        public async Task BuscaNomeCartas_PassaUmNomeParcialDeCarta_RetornaListaDeCartasComONome()
        {
            // Arrange
            var nomeParcial = "hellkite";

            // Act
            var nomes = await _apiService.BuscaNomesDeCarta(nomeParcial);

            // Assert
            Assert.Equal(20, nomes.Count());
            Assert.True(nomes.All(n => n.ToLower().Contains(nomeParcial)));
        }

        [Fact]
        public async Task BuscarTipos_BuscaDosTiposDeArtefatos_RetornaListaComOsTiposDeArtefatos()
        {
            // Arrange
            // Act
            var tipos = await _apiService.BuscaTiposArtefato();
            // Assert
            Assert.Contains(tipos, t => t == "Food");
            Assert.Contains(tipos, t => t == "Clue");
            Assert.Contains(tipos, t => t == "Vehicle");
            Assert.Contains(tipos, t => t == "Treasure");
            Assert.Contains(tipos, t => t == "Equipment");
        }

        [Fact]
        public async Task BuscarTipos_BuscaDosTiposDeCriaturas_RetornaListaComOsTiposDeCriaturas()
        {
            // Arrange
            // Act
            var tipos = await _apiService.BuscaTiposCriatura();
            // Assert
            Assert.Contains(tipos, t => t == "Ninja");
            Assert.Contains(tipos, t => t == "Demon");
            Assert.Contains(tipos, t => t == "Human");
            Assert.Contains(tipos, t => t == "Dragon");
            Assert.Contains(tipos, t => t == "Zombie");
        }
        [Fact]
        public async Task BuscarTipos_BuscaDosTiposDeEncantamentos_RetornaListaComOsTiposDeEncantamentos()
        {
            // Arrange
            // Act
            var tipos = await _apiService.BuscaTiposEncantamento();
            // Assert
            Assert.Contains(tipos, t => t == "Curse");
            Assert.Contains(tipos, t => t == "Aura");
            Assert.Contains(tipos, t => t == "Shrine");
            Assert.Contains(tipos, t => t == "Class");
            Assert.Contains(tipos, t => t == "Saga");
        }
        [Fact]
        public async Task BuscarTipos_BuscaDosTiposDeInstantaneasEFeiticos_RetornaListaComOsTiposDeInstantaneasEFeiticos()
        {
            // Arrange
            // Act
            var tipos = await _apiService.BuscaTiposMagia();
            // Assert
            Assert.Contains(tipos, t => t == "Arcane");
            Assert.Contains(tipos, t => t == "Adventure");
            Assert.Contains(tipos, t => t == "Lesson");
            Assert.Contains(tipos, t => t == "Trap");
        }

        [Fact]
        public async Task BuscarTipos_BuscaDosTiposDeTerrenos_RetornaListaComOsTiposDeTerrenos()
        {
            // Arrange
            // Act
            var tipos = await _apiService.BuscaTiposTerreno();
            // Assert
            Assert.Contains(tipos, t => t == "Forest");
            Assert.Contains(tipos, t => t == "Island");
            Assert.Contains(tipos, t => t == "Mountain");
            Assert.Contains(tipos, t => t == "Plains");
            Assert.Contains(tipos, t => t == "Swamp");
        }

        [Fact]
        public async Task BuscarTipos_BuscaDosTiposDePlaneswalker_RetornaListaComOsTiposDePlaneswalker()
        {
            // Arrange
            // Act
            var tipos = await _apiService.BuscaTiposPlaneswalker();
            // Assert
            Assert.Contains(tipos, t => t == "Gideon");
            Assert.Contains(tipos, t => t == "Liliana");
            Assert.Contains(tipos, t => t == "Garruk");
            Assert.Contains(tipos, t => t == "Jace");
            Assert.Contains(tipos, t => t == "Chandra");
        }

        [Fact]
        public async Task BuscaEdicoes_Buscasimples_RetornaListaDasEdicoes()
        {
            // Arrange
            // Act
            var edicoes = await _apiService.BuscaEdicoes();
            // Assert
            Assert.Contains(edicoes, e => e.Sigla == "war");
            Assert.Contains(edicoes, e => e.Nome == "Kamigawa: Neon Dynasty");

        }




    }
}
