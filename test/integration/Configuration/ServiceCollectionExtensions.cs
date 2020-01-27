using ApiExercise.Api.Configuration.Extensions;
using ApiExercise.Application.Interfaces;
using ApiExercise.Infrastructure.ConnectionString;
using ApiExercise.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Configuration
{
    public static class ServiceCollectionExtensions
    {
        private const string MigrationAssemblyName = "ApiExercise.Infrastructure";

        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<ExerciseContext>(options =>
            {
                var connectionStrings = configuration.GetSection<ConnectionStrings>();
                options.UseSqlServer(connectionStrings.DefaultConnection,
                    builder => 
                    { 
                        builder.MigrationsAssembly(MigrationAssemblyName); 
                    });
            });
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<ExerciseContext>(o =>
                {
                    o.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"],
                        sqlServerOptions => sqlServerOptions.MigrationsAssembly(MigrationAssemblyName));
                })
                .AddScoped<IUnitOfWork>(provider => provider.GetService<ExerciseContext>());
    }
}
