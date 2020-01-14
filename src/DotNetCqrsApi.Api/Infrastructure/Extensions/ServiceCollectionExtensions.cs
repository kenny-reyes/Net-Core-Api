using System;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCqrsApi.Api.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcCoreBuilder AddFluentValidations(this IMvcCoreBuilder builder) =>
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCurrencyValidator>());

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });
        }
        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services,
            IHostingEnvironment environment)
        {
            var isDevelopment = environment.IsDevelopment();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "https://httpstatuses.com/400",
                        Detail = ApiConstants.ModelStateValidation
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes =
                        {
                            "application/problem+json",
                            "application/problem+xml"
                        }
                    };
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
                        Detail = $"{ex.Message} {stackTrace}"
                    };
                });

                config.Map<NotFoundException>((context, ex) =>
                {
                    var stackTrace = isDevelopment ? ex.StackTrace : string.Empty;
                    return new ProblemDetails
                    {
                        Title = ex.Message,
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"{ex.Message} {stackTrace}"
                    };
                });
            });
        }
    }
}