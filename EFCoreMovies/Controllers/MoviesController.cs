using AutoMapper;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id) {
            var movie = await context.Movies
                .Include(m => m.Genres)
                .Include(m => m.cinemaHalls)
                    .ThenInclude(ch => ch.Cinema)
                .Include(m => m.MoviesActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null) {
                return NotFound();
            }

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }
    }
}