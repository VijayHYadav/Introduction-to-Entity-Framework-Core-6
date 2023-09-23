using EFCoreASPNETCOREDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreASPNETCOREDemo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
