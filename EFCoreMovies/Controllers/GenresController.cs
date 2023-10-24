﻿using AutoMapper;
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
        private readonly ILogger<GenresController> logger;

        public GenresController(ApplicationDbContext context, IMapper mapper, ILogger<GenresController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("concurrency_token")]
       public async Task<ActionResult> ConcurrencyToken()
        {

            var genreId = 1;

            try
            {
            // Felipe reads a record from the db
            var genre = await context.Generes.FirstOrDefaultAsync(p => p.Id == genreId);
            genre.Name = "Felipe was here 2";

            // Claudia updates the record in the DB
            await context.Database.ExecuteSqlInterpolatedAsync($@"
                UPDATE Generes SET Example = 'whatever I want 2' 
                WHERE Id = {genreId}");

            // Felipe update the record
            await context.SaveChangesAsync();
                
            } catch(DbUpdateConcurrencyException ex) {
                var entry = ex.Entries.Single();
                var currentGenre = await context.Generes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == genreId);

                foreach (var property in entry.Metadata.GetProperties())
                {
                    var triedValue = entry.Property(property.Name).CurrentValue;
                    var currentDBValue = context.Entry(currentGenre).Property(property.Name).CurrentValue;
                    var previousValue = entry.Property(property.Name).OriginalValue;

                    if (currentDBValue?.ToString() == triedValue?.ToString())
                    {
                        // This is not property that changed
                        continue;
                    }

                    logger.LogInformation($"--- Property {property.Name} ---");
                    logger.LogInformation($"--- Tried value {triedValue} ---");
                    logger.LogInformation($"--- Value in the database {currentDBValue} ---");
                    logger.LogInformation($"--- Previous value {previousValue} ---");

                    // do something...
                }
            }

            return BadRequest("The record was updated by somebody else...");
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
            var genre = await context.Generes.FirstOrDefaultAsync(p => p.Id == id);

            if (genre is null) {
                return NotFound();
            }

            var createdDate  = context.Entry(genre).Property<DateTime>("CreatedDate").CurrentValue;
            var periodStart = context.Entry(genre).Property<DateTime>("PeriodStart").CurrentValue;
            var periodEnd = context.Entry(genre).Property<DateTime>("PeriodEnd").CurrentValue;

            return Ok(new
            {
                Id = genre.Id,
                Name = genre.Name,
                createdDate = createdDate,
                Version = genre.Version,
                periodStart,
                periodEnd
            });
        }

        [HttpGet("temporalAll/{id:int}")]
        public async Task<ActionResult> GetTemporalAll(int id)
        {
            var genres = await context.Generes.TemporalAll()
                .Select(p =>
                new {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(p => p.Id == id).ToListAsync();

            return Ok(genres);
        }

        [HttpGet("temporalAsOf/{id:int}")]
        public async Task<ActionResult> GetTemporalAsOf(int id, DateTime date)
        {
            var genre = await context.Generes.TemporalAsOf(date)
                .Select(p =>
                new {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            return Ok(genre);
        }

        [HttpGet("TemporalFromTo/{id:int}")]
        public async Task<ActionResult> GetTemporalAll(int id, DateTime from, DateTime to)
        {
            var genres = await context.Generes.TemporalFromTo(from, to)
                .Select(p =>
                new {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(p => p.Id == id).ToListAsync();

            return Ok(genres);
        }

        [HttpGet("TemporalContainedIn/{id:int}")]
        public async Task<ActionResult> GetTemporalContainedIn(int id, DateTime from, DateTime to)
        {
            var genres = await context.Generes.TemporalContainedIn(from, to)
                .Select(p =>
                new {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(p => p.Id == id).ToListAsync();

            return Ok(genres);
        }


        [HttpPut("modify_several_times")]
        public async Task<ActionResult> ModifySeveralTimes()
        {
            var genreId = 3;

            var genre = await context.Generes.FirstOrDefaultAsync(x => x.Id == genreId);

            genre.Name = "Comedy 2";
            await context.SaveChangesAsync();
            await Task.Delay(5000);

            genre.Name = "Comedy 3";
            await context.SaveChangesAsync();
            await Task.Delay(5000);

            genre.Name = "Comedy 4";
            await context.SaveChangesAsync();
            await Task.Delay(5000);

            genre.Name = "Comedy 5";
            await context.SaveChangesAsync();
            await Task.Delay(5000);

            genre.Name = "Comedy 6";
            await context.SaveChangesAsync();
            await Task.Delay(5000);

            genre.Name = "Comedy Current";
            await context.SaveChangesAsync();
            await Task.Delay(5000);

            return Ok();
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
                                                    INSERT INTO Genres(Name)
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
