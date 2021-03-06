﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocuco.Application.Services.OcucoHub.LuxotticaRXO;
using Ocuco.Hydra.WebMVC21.V2.Data;
using Ocuco.Hydra.WebMVC21.V2.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Ocuco.Hydra.WebMVC21.V2
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

            services.AddAutoMapper();

            services.AddTransient<IMailService, NullMailService>();

            services.AddTransient<HydraSeeder>();

            services.AddScoped<IHydraRepository, HydraRepository>();


            services.AddScoped<ILuxotticaRXOWSService, LuxotticaRXOWSService>();


            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                ;



            //services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("1.0"));
            //services.AddApiVersioning(o => {
            //    o.ReportApiVersions = true;
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.DefaultApiVersion = new ApiVersion(1, 0);
            //});
            //services.AddApiVersioning(o => {
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.DefaultApiVersion = new ApiVersion(new DateTime(2016, 7, 1));
            //});


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Hydra APIs", Version = "v1" });
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "ToDo API",
            //        Description = "A simple example ASP.NET Core Web API",
            //        TermsOfService = "None",
            //        Contact = new Contact
            //        {
            //            Name = "Cosmos Team",
            //            Email = string.Empty,
            //            Url = "https://twitter.com/spboyer"
            //        },
            //        License = new License
            //        {
            //            Name = "Use under LICX",
            //            Url = "https://example.com/license"
            //        }
            //    });
            //});


            services.AddHttpClient();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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
                    new { controller = "Appl", Action = "Index" });
            });
        }
    }
}
