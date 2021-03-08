using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Interfaces;
using NetCoreApiScaffolding.Domain.Users;
using Microsoft.EntityFrameworkCore;
using NetCoreApiScaffolding.Infrastructure.Context.ModelConfigurations;
using NetCoreApiScaffolding.Tools.Contracts;

namespace NetCoreApiScaffolding.Infrastructure.Context
{
    public class DataBaseContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }

        public DataBaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public static DataBaseContext Create(string connectionString)
        {
            var options = new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
            return new DataBaseContext(options);
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