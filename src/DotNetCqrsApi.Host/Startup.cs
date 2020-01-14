using DotNetCqrsApi.Api;
using DotNetCqrsApi.Host.Extensions;
using DotNetCqrsApi.Host.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCqrsApi.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        readonly string AllowedOrigins = "_AllowedOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services, Configuration, Environment);
            services.AddDistributedMemoryCache();
            services.AddOpenApi();

            services.AddAuthentication(authenticationConfig =>
            {
                authenticationConfig.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authenticationConfig.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("AzureAD", options =>
                {
                    // TODO: Add to the configuration
                    options.Audience = "c98d9236-1912-4678-816d-50b89c42788b";
                    options.Authority = "https://login.microsoftonline.com/6ed241fd-10a3-4a58-b493-6d09931e06e2/v2.0";
                });

            var origins = new string[] { "http://localhost:4200", "https://localhost:4200" };
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins,
                builder =>
                {
                    builder.WithOrigins(origins)
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                });
            });

            services.AddEntityFrameworkCore(Configuration)
                .AddCustomDbContext(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(AllowedOrigins)
                .UseUnitOfWork();

            ApiConfiguration.Configure(app, host =>
            {
                host.UseOpenApi();

                return host;
            });
        }
    }
}