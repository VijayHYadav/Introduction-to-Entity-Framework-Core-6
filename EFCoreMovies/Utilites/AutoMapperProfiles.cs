using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }
    }
}