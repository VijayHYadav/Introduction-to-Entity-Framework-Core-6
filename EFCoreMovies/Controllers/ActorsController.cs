using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper; 

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get() {
            
            return await context.Actors.AsNoTracking()
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreationDTO actorCreationDTO)
        {
            var actor = mapper.Map<Actor>(actorCreationDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ActorCreationDTO actorCreationDTO, int id)
        {
            var actorDB = await context.Actors.FirstOrDefaultAsync(p => p.Id == id);

            if (actorDB is null)
            {
                return NotFound();
            }

            // ! (TRICK) if in these object(actorCreationDTO) we express one change, for example, the biography or the name, then only that property will get updated in that query.
            actorDB = mapper.Map(actorCreationDTO, actorDB);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("disconnected/{id:int}")]
        public async Task<ActionResult> PutDisconnected(ActorCreationDTO actorCreationDTO, int id)
        {
            var existsActor = await context.Actors.AnyAsync(p => p.Id == id);

            if (!existsActor)
            {
                return NotFound();
            }

            var actor = mapper.Map<Actor>(actorCreationDTO);
            actor.Id = id;

            // !  we are going to update all of the properties, all of the columns of the table. It doesn't matter if there wasn't any changes on 
            // ! that column, it is still going to be part of the update query.
            context.Update(actor);

            // ! only for disconnected model
            // context.Entry(actor).Property(p => p.Name).IsModified = true;

            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
