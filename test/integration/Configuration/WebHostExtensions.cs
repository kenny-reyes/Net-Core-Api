using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FunctionalTests.Configuration
{
    public static class WebHostExtensions
    {
        public static IWebHost ExecuteDbContext<TContext>(this IWebHost webHost, Action<TContext> action) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                    action(context);

                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return webHost;
        }
    }
}
