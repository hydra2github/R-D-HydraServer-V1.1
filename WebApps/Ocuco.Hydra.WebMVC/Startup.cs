using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocuco.Hydra.WebMVC.Data;
using Ocuco.Hydra.WebMVC.Services;

namespace Ocuco.Hydra.WebMVC
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }        
        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HydraContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("HydraConnectionString"));
            });

            services.AddTransient<IMailService, NullMailService>();

            services.AddTransient<HydraSeeder>();

            services.AddScoped<IHydraRepository, HydraRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }


            // write text 
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hydra");
            //});


            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseNodeModules(env);

            app.UseMvc(cfg => 
            {
                cfg.MapRoute(
                    "Foo",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });
            });
        }
    }
}
