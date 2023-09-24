# Introduction-to-Entity-Framework-Core-6

```
migration: A migration allows us to express in code the changes that are going to occur in our database.

===========================================================================

>> dotnet ef migrations add initial(initial name is just the name of the migration You can put whatever name you want.)
>> Add-Migration Intial(initial name is just the name of the migration You can put whatever name you want.)

===========================================================================

>> dotnet ef database update
>> update-database
This UPDATE database is going to see all of the migrations that we have and it's going to try to connect to our server.
It is going to first verify if there is already an existing database that corresponds to the connection string that we created.
If there is no database, then it is going to create a new database and it is going to apply all of the migrations that we have in our application.
But if there is already a database that corresponds to that connection string that we created, then it is going to see what are the pending migrations.
Because if there are pending migrations, then we only want to apply those migrations in that database.
And the migrations that have already been applied in the database will not be applied a second time.

>> dotnet ef migrations remove
>> Remove-Migration
It remove the latest migration we can pass some configuration to  it, but for now we're going to keep it simple.

>> dotnet ef migrations remove --force (runs  the down function)
>> Remove-Migration --Force (runs  the down function)
===========================================================================

Agenda
-Create entities
-Configure fields
-Relationships
-3 kinds of  configuratoins

configuration by convention: 
    If a field is called I.D., then automatically it will reconfigure as a primary key.
    A convention means that something will be configure as long as you follow the convention.
    This is called configuration by convention and it allows us to do configurations in entity framework core without having to write code.
    Other convention for having a field mark as a primary key is to say here instead of I.D if I say GenreID, which is the name of the  entity plus ID
    then it is  the same.

// Configuring the Primary Key in 3 Ways

by convention model
public int ID { get; set; } 
by convention model
public int GenreID { get; set; } 

data anotation model
[Key]
public int Identifier { get; set; }

fluent API model
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Genre>().HasKey(p => p.Identifier);
}

Q How can we change the name of our table and schema?
We can do it in two ways.
The first one is using data annotations, and the second one is by using the fluent API.

Data annotations:
namespace EFCoreMovies.Entities
{
    [Table(name: "GenresTbl", Schema = "movies")]
    public class Genre
    {
        public int Id { get; set; }
        [Column("GenreName")]
        public string Name { get; set; }
    }
}

fluent API.
modelBuilder.Entity<Genre>().ToTable(name: "GenresTbl", schema: "movies");
modelBuilder.Entity<Genre>().Property(p => p.Name)
    .HasColumnName("GenreName")
    .HasMaxLength(150).IsRequired();


datetime2: because that is the default convention that says that daytime will map to daytime2.
Now, we don't want that because we're not really going to save the time when the actor was born.
I just want to restore the date, not the time.
daytime it is a struct, which means that it is a value type, not a reference type.
I C sharp value types can't be null 

[Column(TypeName="Date")]
public DateTime? DateOfBirth { get; set; }

modelBuilder.Entity<Actor>().Property(p => p.DateOfBirth).HasColumnType("date");

Configuring Decimal:
[Precision(precision:9, scale: 2)]

modelBuilder.Entity<Cinema>().Property(p => p.Price)
    .HasPrecision(precision: 9, scale: 2);

public DbSet<Movie> Movies { get; set; }
you will be able to query tables of your database through the way db-context, and you need these properties and in order to directly 
query those tables. If I don't have this here, then it will be impossible for me to make a direct and simple query to the movies table.

```