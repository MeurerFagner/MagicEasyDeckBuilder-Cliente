using System.Net.Http.Json;
using System.Text.Json;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public class BaseService
    {
        protected readonly HttpClient _httpClient;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerApi");
        }

        protected async Task<TReturn> PostWithSucessReturn<TModel, TReturn>(string pathUrl, TModel model)
        {
            var result = await _httpClient.PostAsJsonAsync<TModel>(pathUrl, model);
            var dadosRetorno = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var usuarioAuth = JsonSerializer.Deserialize<TReturn>(dadosRetorno, options);

                return usuarioAuth!;
            }

            throw new Exception(dadosRetorno);
        }

        protected async Task PostAsync<TModel>(string pathUrl, TModel model)
        {
            var result = await _httpClient.PostAsJsonAsync<TModel>(pathUrl, model);
            if (!result.IsSuccessStatusCode)
            {
                var dadosRetorno = await result.Content.ReadAsStringAsync();
                throw new Exception(dadosRetorno);
            }
        }
    }
}
