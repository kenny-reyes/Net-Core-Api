using System.Data;
using ApiExercise.Api.Configuration.Extensions;
using ApiExercise.Application.Interfaces;
using ApiExercise.Application.Users;
using ApiExercise.Infrastructure.ConnectionString;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Queries.Shared;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiExercise.Api.Configuration
{
    public static class DependencyResolutions
    {
        public static void AddTo(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(GetUsersRequest).Assembly);

            var connectionStrings = configuration.GetSection<ConnectionStrings>();
            services.AddScoped<IDbConnection>(x =>
                new SqlConnection(connectionStrings.DefaultConnection));

            services.AddScoped<SqlConnection>(x =>
               new SqlConnection(connectionStrings.DefaultConnection));

            services.Scan(scan =>
                scan.FromAssemblyOf<IQuery>().AddClasses(classes => classes.AssignableTo<IQuery>())
                    .AsImplementedInterfaces().WithScopedLifetime());

            services.Scan(scan =>
                scan.FromAssemblyOf<ExerciseContext>().AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces().WithScopedLifetime());
        }
    }
}
