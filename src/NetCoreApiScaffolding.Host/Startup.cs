using System;
using NetCoreApiScaffolding.Api;
using NetCoreApiScaffolding.Tools.Configuration;
using NetCoreApiScaffolding.Tools.Extensions;
using NetCoreApiScaffolding.Tools.Extensions.ApplicationBuilder;
using NetCoreApiScaffolding.Tools.Extensions.Configuration;
using NetCoreApiScaffolding.Tools.Extensions.ServiceCollection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VueCliMiddleware;

namespace NetCoreApiScaffolding.Host
{
    public class Startup
    {
        private const string AllowedOriginsPolicy = "AllowedOriginsPolicy";
        private const string SpaSourcePath = "Spa";
        private const string SpaStaticsPath = "Spa/dist";
        private const string LaunchOnlyApiVariable = "LAUNCH_ONLY_API";

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

            if (IsOnlyApi()) return;
            services.AddSpaStaticFiles(configuration => configuration.RootPath = SpaStaticsPath);
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

            if (IsOnlyApi()) return;
            app.UseStaticFiles();
                
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = SpaSourcePath;
                spa.UseVueCli();
            });
        }

        private static bool IsOnlyApi()
        {
            return bool.Parse((string) Environment.GetEnvironmentVariables()[LaunchOnlyApiVariable] ?? false.ToString());
        }
    }
}