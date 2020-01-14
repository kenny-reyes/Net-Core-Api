using DotNetCqrsApi.Domain.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCqrsApi.Infrastructure.Context.ModelConfigurations.People
{
    public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(u => u.Id)
                .ForSqlServerUseSequenceHiLo($"{nameof(Person)}_hilo");

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Email)
                .HasMaxLength(Person.EmailMaxLenght)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(Person.NameMaxLength)
                .IsRequired();

            builder.Property(u => u.Surname)
                .HasMaxLength(Person.SurnameMaxLength)
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