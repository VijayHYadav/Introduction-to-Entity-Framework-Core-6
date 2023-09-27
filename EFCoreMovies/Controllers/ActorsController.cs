using Azure;
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
        public async Task<IEnumerable<Actor>> Get(int page=1, int recordsToTake = 2) {
            
            return await context.Actors.AsNoTracking().Paginate(page, recordsToTake).ToListAsync();
        }


        
    }
}
