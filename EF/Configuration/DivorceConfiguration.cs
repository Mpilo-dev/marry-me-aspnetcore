using Marry_Me.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marry_Me.EF.Configuration
{
    public class DivorceConfiguration : IEntityTypeConfiguration<Divorce>
    {
        public void Configure(EntityTypeBuilder<Divorce> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.CreatedAt)
                .IsRequired();

            builder.HasOne(d => d.Marriage)
                .WithOne(m => m.Divorce)
                .HasForeignKey<Divorce>(d => d.MarriageId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}



