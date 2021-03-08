using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreApiScaffolding.Domain.Users;

namespace NetCoreApiScaffolding.Infrastructure.Context.ModelConfigurations
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