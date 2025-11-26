using Marry_Me.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marry_Me.EF.Configuration
{
    public class MarriageConfiguration : IEntityTypeConfiguration<Marriage>
    {
        public void Configure(EntityTypeBuilder<Marriage> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.CreatedAt)
                .IsRequired();

            builder.HasOne(m => m.Female)
                .WithMany(p => p.MarriagesAsFemale)
                .HasForeignKey(m => m.FemaleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Male)
                .WithMany(p => p.MarriagesAsMale)
                .HasForeignKey(m => m.MaleId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

