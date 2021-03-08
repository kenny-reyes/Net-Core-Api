using System.Data;
using NetCoreApiScaffolding.Application.Interfaces;
using NetCoreApiScaffolding.Application.Users.GetUsers;
using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Tools.Configuration;
using NetCoreApiScaffolding.Tools.Extensions.Configuration;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreApiScaffolding.Infrastructure.Common.Queries;
using NetCoreApiScaffolding.Tools.Contracts;

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
                scan.FromAssemblyOf<DapperQueryBase>().AddClasses(classes => classes.AssignableTo<IQuery>())
                    .AsImplementedInterfaces().WithScopedLifetime());

            services.Scan(scan =>
                scan.FromAssemblyOf<DataBaseContext>().AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces().WithScopedLifetime());

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<DataBaseContext>());

            return services;
        }
    }
}