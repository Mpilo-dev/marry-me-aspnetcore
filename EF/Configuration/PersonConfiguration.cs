using Marry_Me.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marry_Me.EF.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.IdNumber).IsUnique();


            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Gender)
                .IsRequired();

            builder.Property(p => p.IdNumber)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(p => p.CreatedAt)
                .IsRequired();
        }
    }
}


