﻿using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetCqrsApi.Infrastructure.Context
{
    public static class MyContextInitializer
    {
        public static void Initialize(MyContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
