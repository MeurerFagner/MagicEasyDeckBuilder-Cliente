using MagicEasyDeckBuilderAPI.Client;
using MagicEasyDeckBuilderAPI.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

InjectDependecies(builder);

await builder.Build().RunAsync();

static void InjectDependecies(WebAssemblyHostBuilder builder)
{
    var baseAddress = builder.Configuration.GetValue<string>("UrlApi");
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
}