using System;
using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using ApiExercise.Api;
using FunctionalTests.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests
{
    public class Startup
    {
        private IWebHostEnvironment HostingEnvironment { get; set; }
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            HostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = TestServerDefaults.AuthenticationScheme;
            })
            .AddTestServer();

            ApiConfiguration.ConfigureServices(services, Configuration, HostingEnvironment);
            services.AddEntityFrameworkCore(Configuration)
                .AddCustomDbContext(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ApiConfiguration.Configure(app, env);
        }
    }
}
