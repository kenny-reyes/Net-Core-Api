using System;
using DotNetCqrsApi.Api.Configuration.Extensions;
using DotNetCqrsApi.Domain.Interfaces;
using DotNetCqrsApi.Host.OpenApi;
using DotNetCqrsApi.Infrastructure.ConnectionString;
using DotNetCqrsApi.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using License = System.ComponentModel.License;

namespace DotNetCqrsApi.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.DescribeAllParametersInCamelCase();
                options.CustomSchemaIds(schemaSelector => schemaSelector.FullName);
                options.OperationFilter<ApiVersionOperationFilter>();

                var apiDescriptionProvider =
                    services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var apiVersionDescription in apiDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        apiVersionDescription.GroupName,
                        new Info
                        {
                            Contact = new Contact {Email = "keniakos@hotmail.com"},
                            Version = apiVersionDescription.ApiVersion.ToString(),
                            Title = $"Swagger {apiVersionDescription.ApiVersion}",
                            TermsOfService = String.Empty,
                            //License = new License(),
                            Description = apiVersionDescription.IsDeprecated
                                ? "This API version is deprecated"
                                : String.Empty
                        });
                }
            });
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<MyContext>(o =>
                {
                    o.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], 
                        sqlServerOptions =>
                        {
                            sqlServerOptions.MigrationsAssembly("DotNetCqrsApi.Infrastructure");
                            sqlServerOptions.UseNetTopologySuite();
                        });
                    o.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                })
                .AddScoped<IUnitOfWork>(provider => provider.GetService<MyContext>());
        
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<MyContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                var connectionStrings = configuration.GetSection<ConnectionStrings>();
                options.UseSqlServer(connectionStrings.DefaultConnection,
                    builder =>
                    {
                        builder.MigrationsAssembly("DotNetCqrsApi.Infrastructure");
                        builder.UseNetTopologySuite();
                    });
            });
        }
    }
}