using MagicEasyDeckBuilderAPI.API;
using MagicEasyDeckBuilderAPI.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderApi.Api.Test.Configuracao
{
    public class MEDBAppFactory : WebApplicationFactory<StartupTests>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<Context>));

                services.Remove(descriptor);

                services.AddDbContext<Context>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<Context>();
                    db.Database.EnsureCreated();
                }
            });

            builder.UseEnvironment("Development");
        }
    }
}
