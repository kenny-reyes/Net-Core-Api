using System.Threading.Tasks;
using DotNetCqrsApi.Domain.Interfaces;
using DotNetCqrsApi.Domain.People;
using Microsoft.EntityFrameworkCore;

namespace DotNetCqrsApi.Infrastructure.Context
{
    public class MyContext : DbContext, IUnitOfWork
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Gender> Genders { get; set; }

        public MyContext(DbContextOptions options)
            : base(options)
        {
        }

        public static MyContext Create(string connectionString)
        {
            var options = new DbContextOptionsBuilder().UseSqlServer(
                    connectionString,
                    o => o.UseNetTopologySuite())
                .Options;
            return new MyContext(options);
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

            modelBuilder.ApplyConfiguration(new ModelConfigurations.People.PersonEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfigurations.People.GenderEntityConfiguration());
        }
    }
}
