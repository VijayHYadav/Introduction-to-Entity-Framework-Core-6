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

        public ActorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get(int page=1, int recordsToTake = 2) {
            
            return await context.Actors.AsNoTracking()
                // Select is  call projections
                .Select(a => new ActorDTO { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth })
                .Paginate(page, recordsToTake)
                .ToListAsync();
        }

        [HttpGet("ids")]
        public async Task<IEnumerable<int>> GetIds()
        {

            return await context.Actors.Select(a => a.Id).ToListAsync();
        }

    }
}
