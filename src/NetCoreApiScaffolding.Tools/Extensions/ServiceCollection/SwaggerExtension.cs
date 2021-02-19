using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NetCoreApiScaffolding.Tools.Extensions.ServiceCollection
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection RegisterSwaggerExtensions(this IServiceCollection services, string title, string version)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo {Title = title, Version = version});

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                        "Example: \"Bearer 12345.ExampleToken.abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            UnresolvedReference = true
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var apiDescriptionProvider =
                        app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in apiDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"{apiVersionDescription.GroupName}/swagger.json",
                            $"Version {apiVersionDescription.ApiVersion}"
                        );
                    }
                });
        }
    }
}