using AutoMapper;
using Azure;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using EFCoreMovies.Utilites;
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
        private readonly IMapper mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get() {
            return await context.Generes.AsNoTracking().ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult> Post(GenreCreationDTO genreCreationDTO) {

            /* 
            * Detached: 
                It basically means that entity framework core is not tracking this object.
            * Added:  
                we want to insert a record in the database specifically in the genre's table
            * Unchanged: 
                we have save the changes, then there are no more pending operations on this 
                entity and therefore unchanged is the right status for it to have. Because now if I were 
                to execute save changes one more time ef-core will know that the status  of genre is unchanged 
                and therefore there is nothing to do with it with the database. So no updating records, no adding 
                new records or no deleting any records because the status is unchanged.
            */

            var genre = mapper.Map<Genre>(genreCreationDTO);

            var status1 = context.Entry(genre).State; //* Detached

            // * Now, this add operation is not really adding the genre in the database. I mean, it is not inserting the record in the genre's table.
            // * Please remember that in a previous video I mentioned that EF-framework work with entities by using statuses.
            context.Add(genre); // * what we are doing here is that we are marking genre as added.

            // * context.Genres.Add(genre); more specific way.
            var status2 = context.Entry(genre).State; // * Detached -> Added

            await context.SaveChangesAsync(); // * we are telling EF-Core to check all of the entities that is tracking and check their statuses and apply the corresponding operation.

            var status3 = context.Entry(genre).State; // * Detached -> Unchanged

            return Ok();
        }

        [HttpPost("several")]
        public async Task<ActionResult> Post(GenreCreationDTO[] genresDTO)
        {
            var genres = mapper.Map<Genre[]>(genresDTO);
            context.AddRange(genres);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
