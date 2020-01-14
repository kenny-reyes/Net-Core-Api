﻿using System.Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCqrsApi.Api.Configuration
{
    public static class DependencyResolutions
    {
        public static void AddTo(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(GetCountriesRequest).Assembly);

            var connectionStrings = configuration.GetSection<ConnectionStrings>();
            services.AddScoped<IDbConnection>(x =>
                new SqlConnection(connectionStrings.DefaultConnection));

            services.AddScoped<SqlConnection>(x =>
               new SqlConnection(connectionStrings.DefaultConnection));

            services.Scan(scan =>
                scan.FromAssemblyOf<IQuery>().AddClasses(classes => classes.AssignableTo<IQuery>())
                    .AsImplementedInterfaces().WithScopedLifetime());

            services.Scan(scan =>
                scan.FromAssemblyOf<AppContext>().AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces().WithScopedLifetime());
        }
    }
}