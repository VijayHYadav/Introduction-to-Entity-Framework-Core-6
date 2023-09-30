using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMovies.Entities;

namespace EFCoreMovies.DTOs
{
    public class CinemaHallCreationDTO
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        public CinemaHallType CinemaHallType { get; set; }
    }
}