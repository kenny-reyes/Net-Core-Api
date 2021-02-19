using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Tools.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace NetCoreApiScaffolding.Host
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .MigrateDbContext<DataBaseContext>(ExerciseContextInitializer.Initialize)
                .Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration
                        .SetBasePath(context.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddEnvironmentVariables();
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration.Build())
                        .CreateLogger();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddSerilog(dispose: true);
                })
                .UseStartup<Startup>();
    }
}