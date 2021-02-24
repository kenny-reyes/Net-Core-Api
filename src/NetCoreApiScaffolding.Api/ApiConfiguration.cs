using NetCoreApiScaffolding.Application.Users.CreateUser;
using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Tools.Configuration;
using NetCoreApiScaffolding.Tools.Extensions.Configuration;
using NetCoreApiScaffolding.Tools.Extensions.ServiceCollection;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreApiScaffolding.Api
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services
                .AddApiVersioning(setup =>
                {
                    setup.DefaultApiVersion = new ApiVersion(1, 0);
                    setup.AssumeDefaultVersionWhenUnspecified = true;
                    setup.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer()
                .AddB2CAuthorization()
                .AddMvcCore()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>())
                .AddApiExplorer()
                .Services
                .AddCustomProblemDetails(environment)
                .RegisterEfDatabaseExtension<DataBaseContext>(
                    configuration.GetSection<ConnectionStrings>().DefaultConnection, ServiceLifetime.Scoped)
                .AddDependencies(configuration);

            return services;
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            return app
                .UseUnitOfWork()
                .UseProblemDetails()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}