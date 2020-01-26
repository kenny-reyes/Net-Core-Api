using System;
using ApiExercise.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiExercise.Host.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDbContext(this IWebHost webHost, Action<ExerciseContext> initializer)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ExerciseContext>();

                try
                {
                    initializer(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ExerciseContext>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }
    }
}
