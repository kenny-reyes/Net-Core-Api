using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NetCoreApiScaffolding.Infrastructure.Context
{
    public static class ExerciseContextInitializer
    {
        public static void Initialize(DataBaseContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
