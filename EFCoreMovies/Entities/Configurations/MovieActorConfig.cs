using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class MovieActorConfig : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            // * Composite Primary Key
            builder.HasKey(p => new { p.MovieId, p.ActorId });

            // * n-n relationship
            builder.HasOne(p => p.Actor).WithMany(p => p.MoviesActors).HasForeignKey(p => p.ActorId);

            // * n-n relationship
            builder.HasOne(p => p.Movie).WithMany(p => p.MoviesActors).HasForeignKey(p => p.MovieId);
        }
    }
}

// * Composite Primary Key
// A Composite Primary Key is created by combining two or more columns in a 
// table that can be used to uniquely identify each row in the table when the 
// columns are combined, but it does not guarantee uniqueness when taken individually, 
// or it can also be understood as a primary key created by combining two or more attributes 
// to uniquely identify every row in a table.