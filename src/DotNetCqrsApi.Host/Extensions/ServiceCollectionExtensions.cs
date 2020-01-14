using DotNetCqrsApi.Infrastructure.ConnectionString;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCqrsApi.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<AppContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                var connectionStrings = configuration.GetSection<ConnectionStrings>();
                options.UseSqlServer(connectionStrings.DefaultConnection,
                    builder =>
                    {
                        builder.MigrationsAssembly("DotNetCqrsApi.Infrastructure");
                        builder.UseNetTopologySuite();
                    });
            });
        }
    }
}
