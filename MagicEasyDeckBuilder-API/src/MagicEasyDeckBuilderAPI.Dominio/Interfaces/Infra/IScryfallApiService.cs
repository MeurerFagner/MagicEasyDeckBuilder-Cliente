using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra
{
    public interface IScryfallApiService
    {
        Task<Carta> BucaCartaPorIdScryFall(string idScryfall);
        Task<Carta> BucaCartaPorNome(string nome);
        Task<ConsultaResponseDTO> BuscaCartas(string nome = null, string formato = null, IEnumerable<string> tipos = null, string raridade = null, string edicao = null, string custoMana = null, decimal? valoMana = null, string identidadeDeCor = null, string filtroDeCor = null, string tipoFiltroCor = null, string texto = null, int? page = null);
        Task<IEnumerable<EdicaoDTO>> BuscaEdicoes();
        Task<IEnumerable<string>> BuscaNomesDeCarta(string nome);
        Task<IEnumerable<string>> BuscaTiposArtefato();
        Task<IEnumerable<string>> BuscaTiposCriatura();
        Task<IEnumerable<string>> BuscaTiposEncantamento();
        Task<IEnumerable<string>> BuscaTiposMagia();
        Task<IEnumerable<string>> BuscaTiposPlaneswalker();
        Task<IEnumerable<string>> BuscaTiposTerreno();
    }
}
