using System;
using ApiExercise.Api;
using ApiExercise.Tools.Configuration;
using ApiExercise.Tools.Extensions;
using ApiExercise.Tools.Extensions.ApplicationBuilder;
using ApiExercise.Tools.Extensions.Configuration;
using ApiExercise.Tools.Extensions.ServiceCollection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiExercise.Host
{
    public class Startup
    {
        private const string AllowedOriginsPolicy = "AllowedOriginsPolicy";

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _hostEnvironment = environment ?? throw new ArgumentNullException(nameof(environment));
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services, _configuration, _hostEnvironment)
                .AddDistributedMemoryCache()
                .RegisterSwaggerExtensions(ApiConstants.ApiTitleV1, ApiConstants.ApiVersionV1)
                .AddCors(options => options.AddPolicy(AllowedOriginsPolicy, corsbuilder =>
                    {
                        var allowedOrigins =
                            _configuration.GetSection<Cors>().AllowedOrigins?.Split(";") ??
                            throw new ArgumentNullException(AllowedOriginsPolicy);
                        corsbuilder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(allowedOrigins);
                    }))
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsLocal() || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .AllowAllCorsExtension();
            }
            else
            {
                app.UseExceptionHandler("/Error")
                    .UseHsts()
                    .UseHttpsRedirection()
                    .UseCors(AllowedOriginsPolicy);
            }

            ApiConfiguration.Configure(app, env)
                .UseSwaggerExtension();
        }
    }
}