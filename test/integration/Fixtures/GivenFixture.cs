using System;
using ApiExercise.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Fixtures
{
    public class GivenFixture
    {
        public ExerciseContext DbContext { get; private set; }

        public GivenFixture(HostFixture hostFixture)
        {
            DbContext = hostFixture.Server.Host.Services.GetService<ExerciseContext>();
        }

        public void Reload(HostFixture hostFixture, Action<ExerciseContext> reloadEntities)
        {
            hostFixture.Reset();
            DbContext = hostFixture.Server.Host.Services.GetService<ExerciseContext>();
            reloadEntities(DbContext);
        }
    }
}
