using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public virtual CinemaOffer CinemaOffer { get; set; }
        public virtual HashSet<CinemaHall> CinemaHalls { get; set; }
    }
}

// One Cinema has one CinemaOffer
// One Cinema Has Many CinemHall