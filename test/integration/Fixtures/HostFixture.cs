using System;
using System.Linq;
using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Tools.Configuration;
using NetCoreApiScaffolding.Tools.Extensions.Configuration;
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

            Server.Host.ExecuteDbContext<DataBaseContext>(dbContext =>
            {
                dbContext.Database.EnsureDeleted();
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            });

            Checkpoint.TablesToIgnore = new[] {
                "__EFMigrationsHistory",
                nameof(DataBaseContext.Genders)};
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
