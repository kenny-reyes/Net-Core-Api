using System;
using ApiExercise.Host.OpenApi;
using ApiExercise.Api.Configuration.Extensions;
using ApiExercise.Domain.Interfaces;
using ApiExercise.Infrastructure.ConnectionString;
using ApiExercise.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using License = System.ComponentModel.License;

namespace ApiExercise.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
/*        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.CustomSchemaIds(schemaSelector => schemaSelector.FullName);
                options.OperationFilter<ApiVersionOperationFilter>();

                var apiDescriptionProvider =
                    services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                
                foreach (var apiVersionDescription in apiDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        apiVersionDescription.GroupName,
                        new OpenApiInfo
                        {
                            Contact = new OpenApiContact {Email = "keniakos@hotmail.com"},
                            Version = apiVersionDescription.ApiVersion.ToString(),
                            Title = $"Swagger {apiVersionDescription.ApiVersion}",
                            TermsOfService = new Uri("www.termsServiceUrl"),
                            Description = apiVersionDescription.IsDeprecated
                                ? "This API version is deprecated"
                                : String.Empty
                        });
                }
            });
        }*/
    }
}