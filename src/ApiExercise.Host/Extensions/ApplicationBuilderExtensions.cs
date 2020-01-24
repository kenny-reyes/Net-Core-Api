using System.Net.Http;
using ApiExercise.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace ApiExercise.Host.Extensions
{
    public static class ApplicationBuilderExtensions
    {
/*        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app) =>
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var apiDescriptionProvider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in apiDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                            $"Version {apiVersionDescription.ApiVersion}"
                        );

                        options.RoutePrefix = string.Empty;
                    }
                });*/

        public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app)
        {
            bool IsSuccessStatusCode(HttpResponse response)
            {
                return response.StatusCode >= 200 && response.StatusCode <= 299;
            }

            return app.Use(async (context, next) =>
            {
                await next();

                var requestMethod = context.Request.Method;

                var isSafeMethod = requestMethod == HttpMethod.Get.Method || requestMethod == HttpMethod.Head.Method;

                if (IsSuccessStatusCode(context.Response) && !isSafeMethod)
                {
                    var unitOfWork = context.RequestServices.GetService<IUnitOfWork>();

                    if (unitOfWork != null)
                    {
                        await unitOfWork.Save();
                    }
                }
            });
        }
    }
}