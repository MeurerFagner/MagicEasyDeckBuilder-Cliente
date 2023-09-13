global using Microsoft.AspNetCore.Components.Authorization;
using MagicEasyDeckBuilderAPI.Client;
using MagicEasyDeckBuilderAPI.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MagicEasyDeckBuilderAPI.Client.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

InjectDependencies(builder);
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

static void InjectDependencies(WebAssemblyHostBuilder builder)
{
    var baseAddress = builder.Configuration.GetValue<string>("UrlApi");
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

    builder.Services.AddBlazoredLocalStorage();
}