using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ApiExercise.Infrastructure.Context
{
    public static class ExerciseContextInitializer
    {
        public static void Initialize(ExerciseContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
