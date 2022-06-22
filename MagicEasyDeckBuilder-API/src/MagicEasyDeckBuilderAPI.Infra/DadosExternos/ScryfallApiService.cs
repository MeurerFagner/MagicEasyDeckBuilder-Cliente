using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;
using MagicEasyDeckBuilderAPI.Infra.DadosExternos.SkryfallClassesDeRetorno;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MagicEasyDeckBuilderAPI.Infra.DadosExternos
{
    public class ScryfallApiService : IScryfallApiService
    {
        private const string URL_SCRYFALL_API = "https://api.scryfall.com";
        public async Task<ConsultaResponseDTO> BuscaCartas(
            string nome = null,
            string formato = null,
            IEnumerable<string> tipos = null,
            string raridade = null,
            string edicao = null,
            string custoMana = null,
            decimal? valorMana = null,
            string identidadeDeCor = null,
            string filtroDeCor = null,
            string tipoFiltroCor = null,
            string texto = null,
            int? page = null
            )
        {
            using (var httpClient = new HttpClient())
            {
                var builder = new UriBuilder($"{URL_SCRYFALL_API}/cards/search");
                builder.Port = -1;
                var query = HttpUtility.ParseQueryString(string.Empty);

                var filtros = nome;
                filtros = AdicionaFiltro(filtros, "f:", formato);
                filtros = AdicionaFiltro(filtros, "r:", raridade);
                filtros = AdicionaFiltro(filtros, "e:", edicao);
                filtros = AdicionaFiltro(filtros, "m=", custoMana);
                filtros = AdicionaFiltro(filtros, "cmc=", valorMana.ToString());
                filtros = AdicionaFiltro(filtros, "id:", identidadeDeCor);
                filtros = AdicionaFiltro(filtros, "color"+tipoFiltroCor, filtroDeCor);

                if (tipos != null)
                {
                    foreach (var tipo in tipos)
                    {
                        filtros = AdicionaFiltro(filtros, "t:", tipo);
                    }
                }

                if (texto != null)
                {
                    foreach (var palavra in texto.Split(" "))
                    {
                        filtros = AdicionaFiltro(filtros, "o:", palavra);
                    }
                }

                if (string.IsNullOrEmpty(filtros))
                    throw new ArgumentException("Necessário pelo menos um filtro para buscar cartas");

                query["format"] = "json";
                query["include_extras"] = "false";
                query["include_multilingual"] = "false";
                query["order"] = "name";
                query["page"] = page.HasValue ? page.Value.ToString() : "1";
                query["unique"] = "cards";
                query["q"] = filtros.ToString();

                builder.Query = query.ToString();
                var url = builder.ToString();

                var httpResponse = await httpClient.GetAsync(url);

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                if (!httpResponse.IsSuccessStatusCode)
                    throw new Exception($"Erro {httpResponse.StatusCode} ao buscar cartas em {url}.");

                var content = await httpResponse.Content.ReadAsStringAsync();

                var skryFallReturn = JsonConvert.DeserializeObject<Root>(content);

                var dto = ConvertConsultaCartaScryfall.Map(skryFallReturn);

                return dto;
            }

        }

        private string AdicionaFiltro(string filtroAtual, string tipo, string valor)
        {
            var filtro = new StringBuilder(filtroAtual);

            if (!string.IsNullOrEmpty(valor))
            {
                if (!string.IsNullOrEmpty(filtroAtual))
                    filtro.Append(" ");

                filtro.Append(tipo);
                filtro.Append(valor);
            }

            return filtro.ToString();
        }

        public async Task<Carta> BucaCartaPorNome(string nome)
        {
            using var httpClient = new HttpClient();
            var builder = new UriBuilder($"{URL_SCRYFALL_API}/cards/named");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(string.Empty);

            query["exact"] = nome;

            builder.Query = query.ToString();

            var url = builder.ToString();

            var httpResponse = await httpClient.GetAsync(url);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"Erro {httpResponse.StatusCode} ao buscar cartas em {url}.");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var skryFallReturn = JsonConvert.DeserializeObject<CardData>(content);

            var dto = ConvertConsultaCartaScryfall.Map(skryFallReturn);

            return dto;
        }

        public async Task<Carta> BucaCartaPorIdScryFall(string idScryfall)
        {
            using var httpClient = new HttpClient();

            var url = $"{URL_SCRYFALL_API}/cards/{idScryfall}";

            var httpResponse = await httpClient.GetAsync(url);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"Erro {httpResponse.StatusCode} ao buscar cartas em {url}.");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var skryFallReturn = JsonConvert.DeserializeObject<CardData>(content);

            var dto = ConvertConsultaCartaScryfall.Map(skryFallReturn);

            return dto;
        }

        public async Task<IEnumerable<EdicaoDTO>> BuscaEdicoes()
        {
            using var httpClient = new HttpClient();

            var url = $"{URL_SCRYFALL_API}/sets";

            var httpResponse = await httpClient.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"Erro {httpResponse.StatusCode} ao buscar subtipos em {url}.");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var edicoes = ObtemListaDeDadosNoJson<SetData>(content);

            var dto = edicoes.Where(w => string.IsNullOrEmpty(w.parent_set_code)).Select(e => ConvertConsultaCartaScryfall.Map(e)).ToList();

            return dto;
        }

        public async Task<IEnumerable<string>> BuscaTiposArtefato()
        {
            return await BuscaSubTipos("artifact");
        }

        public async Task<IEnumerable<string>> BuscaTiposCriatura()
        {
            return await BuscaSubTipos("creature");
        }

        public async Task<IEnumerable<string>> BuscaTiposTerreno()
        {
            return await BuscaSubTipos("land");
        }

        public async Task<IEnumerable<string>> BuscaTiposMagia()
        {
            return await BuscaSubTipos("spell");
        }

        public async Task<IEnumerable<string>> BuscaTiposPlaneswalker()
        {
            return await BuscaSubTipos("planeswalker");
        }

        public async Task<IEnumerable<string>> BuscaTiposEncantamento()
        {
            return await BuscaSubTipos("enchantment");
        }

        private async Task<IEnumerable<string>> BuscaSubTipos(string tipo)
        {
            using var httpClient = new HttpClient();

            var url = $"{URL_SCRYFALL_API}/catalog/{tipo}-types";

            var httpResponse = await httpClient.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"Erro {httpResponse.StatusCode} ao buscar subtipos em {url}.");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var tipos = ObtemListaDeDadosNoJson<string>(content);

            return tipos;
        }

        public async Task<IEnumerable<string>> BuscaNomesDeCarta(string nome)
        {
            using var httpClient = new HttpClient();
            var builder = new UriBuilder($"{URL_SCRYFALL_API}/cards/autocomplete");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(string.Empty);

            query["q"] = nome;

            builder.Query = query.ToString();

            var url = builder.ToString();

            var httpResponse = await httpClient.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"Erro {httpResponse.StatusCode} ao buscar cartas em {url}.");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var skryFallReturn = ObtemListaDeDadosNoJson<string>(content);

            return skryFallReturn;
        }

        private static List<T> ObtemListaDeDadosNoJson<T>(string content)
        {
            var jObject = JObject.Parse(content);
            var data = jObject["data"].ToString();
            var skryFallReturn = JsonConvert.DeserializeObject<List<T>>(data);
            return skryFallReturn;
        }
    }
}
