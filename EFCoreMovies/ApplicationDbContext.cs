using EFCoreMovies.Entities;
using EFCoreMovies.Entities.Keyless;
using EFCoreMovies.Entities.Seeding;
using EFCoreMovies.Utilites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// ! tracks gets executed when ef-core starts tracking an entity. meanwhile, the state change gets executed when the state of an entity changes.
namespace EFCoreMovies
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IUserService userService;

        // public ApplicationDbContext() {}

        public ApplicationDbContext(DbContextOptions options, IUserService userService,
            IChangeTrackerEventHandler changeTrackerEventHandler) : base(options)
        {
            this.userService = userService;
            // !  these events will only get fire if we do not use as no tracking.
            if (changeTrackerEventHandler is not null)
            {
                ChangeTracker.Tracked += changeTrackerEventHandler.TrackedHandler;
                ChangeTracker.StateChanged += changeTrackerEventHandler.StateChangeHandler;
                SavingChanges += changeTrackerEventHandler.SavingChangesHandler;
                SavedChanges += changeTrackerEventHandler.SavedChangesHandler;
                SaveChangesFailed += changeTrackerEventHandler.SavedChangesFailHandler;
            }
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlServer("name=DefaultConnection", options => {
        //             options.UseNetTopologySuite();
        //         });
        //     }
        // }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            Module3Seeding.Seed(modelBuilder);
            Module6Seeding.Seed(modelBuilder);
            SomeConfiguraton(modelBuilder);

            // modelBuilder.Ignore<Address>();
            // modelBuilder.Entity<Log>().Property(p => p.Id).ValueGeneratedNever();

            // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            // {
            //     foreach (var property in entityType.GetProperties())
            //     {
            //         if (property.ClrType == typeof(string) && property.Name.Contains("URL",StringComparison.CurrentCultureIgnoreCase)) {
            //             property.SetIsUnicode(false);
            //         }
            //     }
            // }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            ProcessSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSaveChanges() {
            // ! ChangeTracker which allows to take a look into other entities that are being tracked.
            foreach(var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added
                && e.Entity is  AuditableEntity)) {
                    var entity = item.Entity as  AuditableEntity;
                    entity.CreateBy = userService.GetUserId();
                    entity.ModifiedBy = userService.GetUserId();
            }

            // ! ChangeTracker which allows to take a look into other entities that are being tracked.
            foreach(var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified
                && e.Entity is  AuditableEntity)) {
                    var entity = item.Entity as  AuditableEntity;
                    entity.ModifiedBy = userService.GetUserId();
                    // * we are making sure that we never, never, never modify that CreateBy column.
                    item.Property(nameof(entity.CreateBy)).IsModified = false;
            }
        }

        private static void SomeConfiguraton(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CinemaWithoutLocation>().ToSqlQuery("Select Id, Name FROM Cinemas").ToView(null);

            // modelBuilder.Entity<MovieWithCounts>().ToView("MoviesWithCounts");

            modelBuilder.Entity<MovieWithCounts>().ToSqlQuery(@"
            sElEcT Id, Title,
            (select count(*) from GenreMovie where MoviesId = movies.Id) as AmountGenres,
            (select count(distinct moviesID) from CinemaHallMovie
                inner join CinemaHalls
                on CinemaHalls.Id = CinemaHallMovie.cinemaHallsId
                where MoviesId = movies.Id) as AmountCinemas,
            (select count(*) from MoviesActors where MovieId = movies.Id) as AmountActors
            from Movies
            ");

            modelBuilder.Entity<Merchandising>().ToTable("Merchandising");
            modelBuilder.Entity<RentableMovie>().ToTable("RentableMovies");

            var movie1 = new RentableMovie()
            {
                Id = 1,
                Name = "Spider-Man",
                MovieId = 1,
                Price = 5.99m
            };

            var merch1 = new Merchandising()
            {
                Id = 2,
                Available = true,
                IsClothing = true,
                Name = "One Piece T-Shirt",
                Weight = 1,
                Volume = 1,
                Price = 11
            };

            modelBuilder.Entity<Merchandising>().HasData(merch1);
            modelBuilder.Entity<RentableMovie>().HasData(movie1);
        }

        public DbSet<Genre> Generes { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaOffer> CinemaOffers { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
        public DbSet<Log> Logs { get; set; }
        // public DbSet<CinemaWithoutLocation> CinemaWithoutLocations { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CinemaDetail> CinemaDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
