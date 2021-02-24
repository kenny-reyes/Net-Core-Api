using System.Data;
using NetCoreApiScaffolding.Application.Interfaces;
using NetCoreApiScaffolding.Application.Users.GetUsers;
using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Infrastructure.Queries.Contracts;
using NetCoreApiScaffolding.Tools.Configuration;
using NetCoreApiScaffolding.Tools.Extensions.Configuration;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreApiScaffolding.Api
{
    public static class DependencyResolutions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(GetUsersRequest).Assembly);

            var connectionStrings = configuration.GetSection<ConnectionStrings>();
            services.AddScoped<IDbConnection>(x =>
                new SqlConnection(connectionStrings.DefaultConnection));

            services.AddScoped(x =>
                new SqlConnection(connectionStrings.DefaultConnection));

            services.Scan(scan =>
                scan.FromAssemblyOf<IQuery>().AddClasses(classes => classes.AssignableTo<IQuery>())
                    .AsImplementedInterfaces().WithScopedLifetime());

            services.Scan(scan =>
                scan.FromAssemblyOf<DataBaseContext>().AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces().WithScopedLifetime());

            return services;
        }
    }
}