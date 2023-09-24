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

            modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(150).IsRequired();
        }

        public DbSet<Genre> Generes { get; set; }
    }
}
