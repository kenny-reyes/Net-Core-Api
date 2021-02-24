using NetCoreApiScaffolding.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCoreApiScaffolding.Infrastructure.Context.ModelConfigurations.Users
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id)
                .UseHiLo($"{nameof(User)}_hilo");

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Email)
                .HasMaxLength(User.EmailMaxLength)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(User.NameMaxLength)
                .IsRequired();

            builder.Property(u => u.Birthdate)
                .IsRequired();

            builder.Property(u => u.GenderId)
                .HasDefaultValue(Gender.Unknown.Id);

            builder.HasOne(u => u.Gender)
                .WithMany()
                .HasForeignKey(u => u.GenderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}