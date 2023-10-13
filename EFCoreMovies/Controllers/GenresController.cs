using AutoMapper;
using Azure;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using EFCoreMovies.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        [HttpPost("concurrency_token")]
       public async Task<ActionResult> ConcurrencyToken()
        {

            var genreId = 1;

            // Felipe reads a record from the db
            var genre = await context.Generes.FirstOrDefaultAsync(p => p.Id == genreId);
            genre.Name = "Felipe was here";

            // Claudia updates the record in the DB
            await context.Database.ExecuteSqlInterpolatedAsync($@"
                UPDATE Generes SET Name = 'Claudia I want 2' 
                WHERE Id = {genreId}");

            // Felipe update the record
            await context.SaveChangesAsync();

            return Ok();


        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get() {
            context.Logs.Add(new Log { Message = "Executing Get from GenresController" });
            // * context.Logs.Add(new Log { Id = Guid.NewGuid(), Message = "Executing Get from GenresController" });
            await context.SaveChangesAsync();
            return await context.Generes
                .AsNoTracking()
                // ! this is how we can short by Shadow Property
                // .OrderByDescending(g => EF.Property<DateTime>(g, "CreatedDate"))
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genre>> Get(int id){
            // var genre = await context.Generes.FirstOrDefaultAsync(p => p.Id == id);

            // var genre = await context.Generes.FromSqlRaw("SeleCt * From Generes Where Id = {0}", id).IgnoreQueryFilters().FirstOrDefaultAsync();

            var genre = await context.Generes.FromSqlInterpolated($"SeleCt * From Generes Where Id = {id}").IgnoreQueryFilters().FirstOrDefaultAsync();

            if (genre is null) {
                return NotFound();
            }

            var createdDate  = context.Entry(genre).Property<DateTime>("CreatedDate").CurrentValue;

            return Ok(new
            {
                Id = genre.Id,
                Name = genre.Name,
                createdDate = createdDate
            });
        }

        [HttpPost("add2")]
        public async Task<ActionResult> Add2(int id)
        {
            var genre = await context.Generes.FirstOrDefaultAsync(p => p.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.Name += " 2";
            await context.SaveChangesAsync();
            return Ok();
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
            
            var genreExists = await context.Generes.AnyAsync(p => p.Name == genreCreationDTO.Name);

            if (genreExists) return BadRequest($"The genre with name {genreCreationDTO.Name} already exists.");

            var genre = mapper.Map<Genre>(genreCreationDTO);

            var status1 = context.Entry(genre).State; //* Detached

            // * Now, this add operation is not really adding the genre in the database. I mean, it is not inserting the record in the genre's table.
            // * Please remember that in a previous video I mentioned that EF-framework work with entities by using statuses.
            // context.Add(genre); // * what we are doing here is that we are marking genre as added.

            
            await context.Database.ExecuteSqlInterpolatedAsync($@"
                                                    INSERT INTO Generes(Name)
                                                    VALUES({genre.Name})");

            // context.Entry(genre).State = EntityState.Added;

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

        [HttpPut]
        public async Task<ActionResult> Put(Genre genre) {
            context.Update(genre);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await context.Generes.FirstOrDefaultAsync(p => p.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            // ! This remove the genre that we have here is the one that changes the status of the genre entity into deleted.
            context.Remove(genre);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("softdelete/{id:int}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var genre = await context.Generes.FirstOrDefaultAsync(p => p.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.isDeleted = true;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("restore/{id:int}")]
        public async Task<ActionResult> Restore(int id)
        {
            // ! This is how we can ignore all of them query filtersIgnoreQueryFilters.
            var genre = await context.Generes.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.isDeleted = false;
            await context.SaveChangesAsync();
            return Ok();
        }


    }
}
