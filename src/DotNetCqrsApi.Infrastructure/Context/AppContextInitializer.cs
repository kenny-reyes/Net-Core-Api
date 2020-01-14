using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetCqrsApi.Infrastructure.Context
{
    public static class AppContextInitializer
    {
        public static void Initialize(MyContext context)
        {
            RunMigrations(context);
        }

        private static void RunMigrations(DbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
