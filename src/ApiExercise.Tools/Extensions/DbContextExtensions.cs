using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiExercise.Tools.Extensions
{
    public static class DbContextExtensions
    {
        public static void ReloadEntity<TEntity>(this DbContext context, TEntity entity)
            where TEntity : class
        {
            context.Entry(entity).Reload();
        }

        public static Task ReloadNavigationProperty<TEntity, TElement>(
            this DbContext context,
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TElement>>> navigationProperty)
            where TEntity : class
            where TElement : class
        {
            return context.Entry(entity).Collection(navigationProperty).Query().LoadAsync();
        }
    }
}