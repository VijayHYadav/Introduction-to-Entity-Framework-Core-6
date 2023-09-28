using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                .Include(m => m.Genres.OrderByDescending(g => g.Name).Where(g => !g.Name.Contains("m")))
                .Include(m => m.cinemaHalls.OrderByDescending(ch => ch.Cinema.Name))
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

        [HttpGet("automapper/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithAutoMapper(int id) {

            var movieDTO = await context.Movies
                .ProjectTo<MovieDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDTO is null) {
                return NotFound();
            }

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("selectloading/{id:int}")]
        public async Task<ActionResult> GetSelectLoading(int id) {

            var movieDTO = await context.Movies.Select(m => new
            {
                Id = m.Id,
                Title = m.Title,
                Genres =  m.Genres.Select(g => g.Name).OrderByDescending(n => n).ToList()
            }).FirstOrDefaultAsync(m => m.Id == id);

            /*
            {
                "id": 3,
                "title": "Spider-Man: No way home",
                "genres": [
                    "Science Fiction",
                    "Comedy",
                    "Action"
                ]
            }
            */

            //* var movieDTO = await context.Movies.Select(m => new MovieDTO
            //* {
            //*     Id = m.Id,
            //*     Title = m.Title,
            //*     Genres =  mapper.Map<List<GenreDTO>>(m.Genres.OrderByDescending(g => g.Name))
            //* }).FirstOrDefaultAsync(m => m.Id == id);

            /*
            {
                "id": 3,
                "title": "Spider-Man: No way home",
                "genres": [
                    {
                    "id": 4,
                    "name": "Science Fiction"
                    },
                    {
                    "id": 3,
                    "name": "Comedy"
                    },
                    {
                    "id": 1,
                    "name": "Action"
                    }
                ],
                "cinemas": null,
                "actors": null
            }
            */

            if (movieDTO is null) return NotFound();

            return Ok(movieDTO);
        }

        [HttpGet("explicitLoading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetExplicit(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            // await context.Entry(movie).Collection(p => p.Genres).LoadAsync()
            var genresCount = await context.Entry(movie).Collection(p => p.Genres).Query().CountAsync();

            var movieDTO = mapper.Map<MovieDTO>(movie);

            return Ok(new
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                GenresCount = genresCount
            });
        }

        // I don't really see the real benefit of using something like lazy loading.
        // You can definitely load the related data in subsequent queries if you want using something like Explicit Loading, Deferred Execution
        [HttpGet("lazyloading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetLazyLoading(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;

            // var movies = await context.Movies.ToListAsync();
            // n + 1 problem
            // foreach (var movie in movies)
            // {
            //     // loading the  genres of the 'movie'
            //     movie.Genres.ToList();
            // }
        }
    }
}