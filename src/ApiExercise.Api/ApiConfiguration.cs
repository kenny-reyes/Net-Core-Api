using ApiExercise.Api.Configuration;
using ApiExercise.Api.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiExercise.Api
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services
                .AddCustomApiVersioning()
                .AddVersionedApiExplorer()
                .AddMvcCore()
                .AddAuthorization()
                .AddFluentValidations()
                .AddApiExplorer()
                .Services
                .AddCustomProblemDetails(environment)
                .AddCustomDbContext(configuration);

            DependencyResolutions.AddTo(services, configuration);

            return services;
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            return app
                .UseUnitOfWork()
                .UseProblemDetails()
                .UseAuthentication();
        }
    }
}