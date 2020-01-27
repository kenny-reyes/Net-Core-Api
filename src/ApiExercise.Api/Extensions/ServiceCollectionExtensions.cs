using System;
using ApiExercise.Api.Configuration.Extensions;
using ApiExercise.Application.Interfaces;
using ApiExercise.Application.Users;
using ApiExercise.Domain.Exceptions;
using ApiExercise.Infrastructure.ConnectionString;
using ApiExercise.Infrastructure.Context;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiExercise.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string MigrationsAssemblyName = "ApiExercise.Infrastructure";

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<ExerciseContext>(options =>
                {
                    options.EnableSensitiveDataLogging(); // Be careful with this

                    var connectionStrings = configuration.GetSection<ConnectionStrings>();
                    options.UseSqlServer(connectionStrings.DefaultConnection,
                        sqlServerOptions => { sqlServerOptions.MigrationsAssembly(MigrationsAssemblyName); });

                    options.ConfigureWarnings(warnings =>
                        warnings.Throw(RelationalEventId.QueryPossibleExceptionWithAggregateOperatorWarning));
                })
                .AddScoped<IUnitOfWork>(provider => provider.GetService<ExerciseContext>());
        }

        public static IMvcCoreBuilder AddFluentValidations(this IMvcCoreBuilder builder) =>
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>());

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
            IWebHostEnvironment environment)
        {
            var isDevelopment = environment.IsDevelopment();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Type = "https://httpstatuses.com/400",
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Bad Request",
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
                        Detail = $"{ex.Message} {stackTrace}"
                    };
                });
            });
        }
    }
}