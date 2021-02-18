using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ApiExercise.Tools.Extensions
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
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app, string title, string version)
        {
            return app.UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/{version}/swagger.json", title); });
        }
    }
}