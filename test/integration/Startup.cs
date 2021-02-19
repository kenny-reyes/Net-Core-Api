using System;
using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using ApiExercise.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services, _configuration, _hostingEnvironment)
                .AddAuthentication(options => options.DefaultScheme = TestServerDefaults.AuthenticationScheme)
                .AddTestServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ApiConfiguration.Configure(app, env)
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers()); 
        }
    }
}
