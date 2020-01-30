using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace ApiExercise.Host.Extensions
{
    namespace Microsoft.AspNetCore.Builder
    {
        public static class ApplicationBuilderExtensions
        {
            public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app) =>
                app
                    .UseSwagger()
                    .UseSwaggerUI(options =>
                    {
                        /*
                         * NOTE: We only have a version but we already prepare for more 
                         */
                        var apiDescriptionProvider =
                            app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                        foreach (var apiVersionDescription in apiDescriptionProvider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                                $"Version {apiVersionDescription.ApiVersion}"
                            );

                            options.RoutePrefix = string.Empty;
                        }
                    });
        }
    }
}
