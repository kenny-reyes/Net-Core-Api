using frontend.Models;
using Microsoft.EntityFrameworkCore;

namespace frontend.Contexts
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasData(new Customer() {Id = 1, Name = "Microsoft", Vat = "IE8256796U", Enabled = true},
                    new Customer() {Id = 2, Name = "Google", Vat = "IE6388047V", Enabled = false});
        }
    }
}
