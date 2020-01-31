using System;
using ApiExercise.Api;
using ApiExercise.Host.Extensions.Microsoft.AspNetCore.Builder;
using ApiExercise.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ApiExercise.Api.Extensions;

namespace ApiExercise.Host
{
    public class Startup
    {
        private const string AllowedOriginsPolicy = "AllowedOriginsPolicy";

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services, Configuration, Environment)
                .AddDistributedMemoryCache()
                .AddCors(options =>
                {
                    options.AddPolicy(AllowedOriginsPolicy,
                        corsbuilder =>
                        {
                            var allowedOrigins =
                                Configuration.GetSection<CorsConfiguration>().AllowedOrigins.Split(";") ??
                                throw new ArgumentNullException(AllowedOriginsPolicy);
                            corsbuilder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithOrigins(allowedOrigins);
                        });
                })
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
                
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(ApiConstants.ApiVersionV1,
                        new OpenApiInfo
                        {
                            Title = ApiConstants.ApiTitleV1,
                            Version = ApiConstants.ApiVersionV1
                        });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage()
                    .UseCors(AllowedOriginsPolicy)
                    .UseHttpsRedirection();
            }
            else
            {
                app.UseCors(cors => cors
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            }

            ApiConfiguration.Configure(app, env)
                .UseRouting()
                .UseAuthorization()
                .UseOpenApi()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}