using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetCoreApiScaffolding.Tools.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDbContext<T>(this IWebHost webHost, Action<T> initializer) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<T>();

                try
                {
                    initializer(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<T>>();
                    logger.LogError(ex, "An error occurred while migrating the database");
                }
            }

            return webHost;
        }
    }
}
