using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;

using static System.Console;
using Microsoft.AspNetCore.Mvc.Formatters;
using NorthwindService.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace NorthwindService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddHealthChecks().AddDbContextCheck<Northwind>();

            string databasePath = Path.Combine("..", "Northwind.db");
            services.AddDbContext<Northwind>(options => options.UseSqlite($"Data Source={databasePath}"));

            services.AddControllers(options => {
                WriteLine("Default output formatters:");
                foreach(IOutputFormatter formatter in options.OutputFormatters){
                    var mediaFormatter = formatter as OutputFormatter;

                    if(mediaFormatter == null){
                        WriteLine($"    {formatter.GetType().Name}");
                    }
                    else{
                        WriteLine($"    {0}, Media type: {1}", 
                                    mediaFormatter.GetType().Name,
                                    string.Join(", ", mediaFormatter.SupportedMediaTypes));
                    }
                }
            })
                    .AddXmlDataContractSerializerFormatters()
                    .AddXmlSerializerFormatters()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddSwaggerGen(options =>{
                options.SwaggerDoc(
                    name: "v1", 
                    info: new OpenApiInfo{
                        Title = "Northwind Service API",
                        Version = "v1"
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks(path: "/howdoyoufeel");

            app.UseCors(options => {
                options.WithMethods("GET", "POST", "PUT", "DELETE");
                options.WithOrigins("https://localhost:5002");
            });

            app.Use(next => (context) => {
                var endpoint = context.GetEndpoint();

                if(endpoint != null){
                    WriteLine("*** Name: {0}; Route: {1}; Metadata: {2}",
                    endpoint.DisplayName,
                    (endpoint as RouteEndpoint)?.RoutePattern,
                    string.Join(", ", endpoint.Metadata));
                }
                
                return next(context);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>{
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");

                options.SupportedSubmitMethods(new[] {
                    SubmitMethod.Get, SubmitMethod.Post,
                    SubmitMethod.Put, SubmitMethod.Delete
                });
            });

        }
    }
}
