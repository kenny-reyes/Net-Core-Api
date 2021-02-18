using System;
using System.Net.Http;
using System.Reflection;
using ApiExercise.Tools.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiExercise.Tools.Extensions
{
    public static class EfDatabaseExtensions
    {
        public static IServiceCollection RegisterEfDatabaseExtension<T>(this IServiceCollection services, string connectionString,
            ServiceLifetime serviceLifetime) where T : DbContext
        {
            var migrationsAssemblyName = typeof(T).GetTypeInfo().Assembly.GetName().Name;
            return services.AddDbContext<T>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(
                        connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(migrationsAssemblyName);
                            sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                            sqlOptions.CommandTimeout(60);
                        });
                },
                serviceLifetime // Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );
        }

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