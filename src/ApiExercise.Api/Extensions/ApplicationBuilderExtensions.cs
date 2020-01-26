using System.Net.Http;
using ApiExercise.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ApiExercise.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
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