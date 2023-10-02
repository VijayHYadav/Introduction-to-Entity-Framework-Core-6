using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMovies.Entities.Configurations
{
    public class CinemaConfig : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            // 1-1 Relationship
            builder.HasOne(c => c.CinemaOffer).WithOne().HasForeignKey<CinemaOffer>(co =>co.CinemaId);
        }
    }
}
