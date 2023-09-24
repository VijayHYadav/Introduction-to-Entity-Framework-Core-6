using EFCoreMovies.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().Property(p => p.Name)
                .HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Actor>().Property(p => p.Name)
                .HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Actor>().Property(p => p.DateOfBirth).HasColumnType("date");

            modelBuilder.Entity<Cinema>().Property(p => p.Name)
                .HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Cinema>().Property(p => p.Price).HasColumnType("decimal")
                .HasPrecision(precision: 9, scale: 2);
        }

        public DbSet<Genre> Generes { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
    }
}
