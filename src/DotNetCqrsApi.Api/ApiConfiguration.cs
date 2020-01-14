using System;
using DotNetCqrsApi.Api.Configuration;
using DotNetCqrsApi.Api.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace DotNetCqrsApi.Api
{
    public static class ApiConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
        {
            services
                .AddCustomApiVersioning()
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters()
                .AddFluentValidations()
                .AddApiExplorer()
                .AddVersionedApiExplorer()
                .Services
                .AddCustomProblemDetails(environment);

            DependencyResolutions.AddTo(services, configuration);
        }

        public static void Configure(IApplicationBuilder app,
            Func<IApplicationBuilder, IApplicationBuilder> configureHost)
        {
            configureHost(app)
                .UseProblemDetails()
                .UseAuthentication()
                .UseMvc();
        }
    }
}