global using Microsoft.AspNetCore.Components.Authorization;
using MagicEasyDeckBuilderAPI.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MagicEasyDeckBuilderAPI.Client.Providers;
using MagicEasyDeckBuilderAPI.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

InjectDependencies(builder);
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

static void InjectDependencies(WebAssemblyHostBuilder builder)
{
    var baseAddress = builder.Configuration.GetValue<string>("UrlApi");
    builder.Services.AddTransient<AutheticationRequestDelegatingHandler>();
    builder.Services
        .AddHttpClient("ServerApi", client => client.BaseAddress = new Uri(baseAddress))
        .AddHttpMessageHandler<AutheticationRequestDelegatingHandler>();

    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    builder.Services.AddScoped<IDeckService, DeckService>();
    builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

#if DEBUG
    builder.Services.AddSassCompiler();
#endif

    builder.Services.AddBlazoredLocalStorage();
}