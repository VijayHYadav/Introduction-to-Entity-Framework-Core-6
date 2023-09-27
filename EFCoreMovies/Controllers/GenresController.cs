using Azure;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return await context.Generes.ToListAsync();
        }
    }
}
