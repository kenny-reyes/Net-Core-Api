using System;
using System.Linq;
using ApiExercise.Api.Extensions;
using ApiExercise.Infrastructure.Configuration;
using ApiExercise.Infrastructure.Context;
using FunctionalTests.Configuration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace FunctionalTests.Fixtures
{
    public class HostFixture : IDisposable
    {
        private static readonly Checkpoint Checkpoint = new Checkpoint();
        private static ConnectionStrings _connectionString;

        public TestServer Server { get; private set; }

        public HostFixture()
        {
            Server = TestServerBuilder.Create().WithStartup<Startup>().Build();

            _connectionString = Server.Host.Services.GetService<IConfiguration>().GetSection<ConnectionStrings>();

            Server.Host.ExecuteDbContext<ExerciseContext>(dbContext =>
            {
                dbContext.Database.EnsureDeleted();
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            });

            Checkpoint.TablesToIgnore = new[] {
                "__EFMigrationsHistory",
                nameof(ExerciseContext.Genders)};
        }

        public void Reset()
        {
            Server = TestServerBuilder.Create().WithStartup<Startup>().Build();
        }

        public static void ResetCheckpoint() => Checkpoint.Reset(_connectionString.DefaultConnection).Wait();

        public void Dispose()
        {
            Server.Dispose();
        }
    }
}
