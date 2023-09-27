using AutoMapper;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;

namespace EFCoreMovies.Utilites
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDTO>();
            // .NET number values such as positive and negative infinity cannot be written as valid JSON. 
            CreateMap<Cinema, CinemaDTO>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(p => p.Location.Y))
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(p => p.Location.X));

            CreateMap<Genre, GenreDTO>();

            // System.ArgumentException: .NET number values such as positive and negative infinity cannot be written as valid JSON. To make it work when using 'JsonSerializer', consider specifying 'JsonNumberHandling.AllowNamedFloatingPointLiterals'
            CreateMap<Movie,MovieDTO>()
                .ForMember(dto  => dto.Cinemas, ent =>  ent.MapFrom(p => p.cinemaHalls.Select(c  => c.Cinema)))
                .ForMember(dto => dto.Actors, ent => ent.MapFrom(p => p.MoviesActors.Select(ma => ma.Actor)));
        }
    }
}