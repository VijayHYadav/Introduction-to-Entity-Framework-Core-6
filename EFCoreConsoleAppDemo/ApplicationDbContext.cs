using EFCoreConsoleAppDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleAppDemo
{
    // we're going to do it inside of the DBcontext
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=EFCoreConsoleAppDemoDB.db");
        }

        public DbSet<Person> people { get; set; }
    }
}

/*

Now, there are several ways in which we can configure this DB context to either use SQL Server or use Oracle, Postgres or whatever.
And also we can indicate the connection string to connect to our database.
I can do that from outside of the DBcontext or inside of the DBcontext.
In this video we're going to do it inside of the DBcontext and in the next video we're going to do it outside of the DB context.

So I need to overwrite, I will say overwrite a method call or configuring this on configuring will
allow me to configure entity framework core

I will say options builder and here I will say use SQL Server and this used SQL Server as its name implies,
is what allows me to say that entity framework or will connect to a SQL Server database and here I can pass the connection string.
Of course, you should put this connection string in a configuration provider like a JSON file or an environment variable, but for 
this video, we're going to keep it simple and we're just going to write


These integrated security means that if we want to use our Windows credentials to log in to the database
server or if we want to use a combination of username and password, in my case, I will just use my Windows credentials.
 
*/