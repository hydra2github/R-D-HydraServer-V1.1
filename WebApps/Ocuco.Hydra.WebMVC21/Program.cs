using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocuco.Hydra.WebMVC.Data;

namespace Ocuco.Hydra.WebMVC21
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // .NET Core 2.0
            //BuildWebHost(args).Run();
            //var host = BuildWebHost(args);
            //RunSeeding(host);
            //host.Run();

            // .NET Core 2.1
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }


        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<HydraSeeder>();
                seeder.Seed();
            }
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration(SetupConfiguration)
                   .UseStartup<Startup>();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Removing the default configuration options
            builder.Sources.Clear();

            //builder.AddJsonFile("config.json", false, true)
            //       .AddXmlFile("config.xml", true)
            //       .AddEnvironmentVariables();

            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables();
        }
    }
}
