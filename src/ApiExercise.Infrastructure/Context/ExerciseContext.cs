using System.Threading.Tasks;
using ApiExercise.Application.Interfaces;
using ApiExercise.Domain.Users;
using ApiExercise.Infrastructure.Context.ModelConfigurations.Users;
using Microsoft.EntityFrameworkCore;

namespace ApiExercise.Infrastructure.Context
{
    public class ExerciseContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }
        
        public ExerciseContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public static ExerciseContext Create(string connectionString)
        {
            var options = new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
            return new ExerciseContext(options);
        }

        public async Task Save()
        {
            if (ChangeTracker.HasChanges())
            {
                await SaveChangesAsync();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GenderEntityConfiguration());
        }
    }
}