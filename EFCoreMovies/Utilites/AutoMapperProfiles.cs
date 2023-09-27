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
        }
    }
}