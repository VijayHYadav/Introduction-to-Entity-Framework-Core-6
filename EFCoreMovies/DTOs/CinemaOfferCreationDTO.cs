using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.DTOs
{
    public class CinemaOfferCreationDTO
    {
        [Range(1, 100)]
        public double DiscountPercentage { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}