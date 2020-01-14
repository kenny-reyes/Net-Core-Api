using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCqrsApi.Host.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDbContext(this IWebHost webHost, Action<AppContext> initializer)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppContext>();

                try
                {
                    initializer(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppContext>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }
    }
}
