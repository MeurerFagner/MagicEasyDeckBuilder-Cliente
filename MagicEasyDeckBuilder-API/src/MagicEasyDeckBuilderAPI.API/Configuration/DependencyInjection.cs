using MagicEasyDeckBuilderAPI.App.Mapper;
using MagicEasyDeckBuilderAPI.App.Services;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;
using MagicEasyDeckBuilderAPI.Infra;
using MagicEasyDeckBuilderAPI.Infra.DadosExternos;
using MagicEasyDeckBuilderAPI.Infra.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace MagicEasyDeckBuilderAPI.API.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IDeckRepository, DeckRepository>();
            services.AddScoped<IDeckAppService, DeckAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<ICartaAppService, CartaAppService>();
            services.AddScoped<IScryfallApiService, ScryfallApiService>();
            services.AddScoped<TokenService>();
            services.AddScoped<Context>();

            services.AddAutoMapper(m => m.AddProfile(new MapperProfile()));
        }
    }
}
