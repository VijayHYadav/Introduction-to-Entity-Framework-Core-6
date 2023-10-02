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

            // 1-n Relationship
            builder.HasMany(c => c.CinemaHalls).WithOne(ch => ch.Cinema).HasForeignKey(co =>co.TheCinemaId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CinemaDetail).WithOne(c => c.Cinema).HasForeignKey<CinemaDetail>(cd => cd.Id);

        }
    }
}

// * OnDelete
// Cascade :  The dependent entities are deleted if the principal entity is also deleted
// Client Cascade
// No Action : no action
// Client No Action
// Restrict : the action that is going to happen in the principal entity is not going to be done in the dependent entities.
// Set Null :  In this case we simply put null in the column of the foreign key
// Client Set Null :  we put null from the applications.
