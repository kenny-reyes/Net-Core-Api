using ApiExercise.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiExercise.Infrastructure.Context.ModelConfigurations.Users
{
    public class GenderEntityConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedNever();
            builder.Property(r => r.Name).HasMaxLength(Gender.NameMaxLength).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();
            builder.HasData(Gender.GetGenders());
        }
    }
}
