using System;
using ApiExercise.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Fixtures
{
    public class GivenFixture
    {
        public DataBaseContext DbContext { get; private set; }

        public GivenFixture(HostFixture hostFixture)
        {
            DbContext = hostFixture.Server.Host.Services.GetService<DataBaseContext>();
        }

        public void Reload(HostFixture hostFixture, Action<DataBaseContext> reloadEntities)
        {
            hostFixture.Reset();
            DbContext = hostFixture.Server.Host.Services.GetService<DataBaseContext>();
            reloadEntities(DbContext);
        }
    }
}
