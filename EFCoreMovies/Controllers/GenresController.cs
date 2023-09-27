using Azure;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GenresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get() {
            // here we are doing a synchronous operation
            // But since consulting a database is an io
            // operation, the good practices says that we should use asynchronous programming
            // The good thing is that entity framework core can execute asynchronous operations very easily.
            //return context.Generes.ToList();

            //entity fw core keeps a track of the entities that were loaded from the database.
            //This will allow us, for example, to update those records or delete them in a really easy manner.
            //So if we only want to do a read only operation, then we can skip the tracking and make our application
            //more performant. For that, we use a method called AsNoTracking.
            return await context.Generes.AsNoTracking().ToListAsync();
        }

        [HttpGet("first")]
        public async Task<ActionResult<Genre>> GetFirst()
        {
            
            var genre = await context.Generes.FirstOrDefaultAsync(g => g.Name.Contains("m"));

            if (genre is null)
            {
                return NotFound();
            }

            return genre;
        }
    }
}
