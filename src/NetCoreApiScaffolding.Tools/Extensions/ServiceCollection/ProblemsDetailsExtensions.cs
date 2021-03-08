using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreApiScaffolding.Tools.Exceptions;

namespace NetCoreApiScaffolding.Tools.Extensions.ServiceCollection
{
    public static class ProblemDetailsExtensions
    {
        private const string ModelStateValidation = "Please refer to the errors property for additional details.";

        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services, IWebHostEnvironment environment)
        {
            var isDevelopment = environment.IsDevelopment() || environment.IsLocal();

            services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Instance = context.HttpContext.Request.Path,
                    Type = "https://httpstatuses.com/400",
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = ModelStateValidation
                };

                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes =
                    {
                        "application/problem+json",
                        "application/problem+xml"
                    }
                };
            });

            return services.AddProblemDetails(config =>
            {
                config.IncludeExceptionDetails = ctx => isDevelopment;

                config.Map<Exception>((context, ex) =>
                {
                    var stackTrace = isDevelopment ? ex.StackTrace : string.Empty;
                    return new ProblemDetails
                    {
                        Title = ex.Message,
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = $"{stackTrace}"
                    };
                });

                config.Map<NotFoundException>((context, ex) =>
                {
                    var stackTrace = isDevelopment ? ex.StackTrace : string.Empty;
                    return new ProblemDetails
                    {
                        Title = ex.Message,
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"{stackTrace}"
                    };
                });
            });
        }
    }
}