using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public CinemaHallType CinemaHallType { get; set; }
        public decimal Cost { get; set; }
        public Currency Currency { get; set; }
        public int TheCinemaId { get; set; }
        // If I have a cinema hall, I will be able to query
        // the data of the corresponding cinema with a single
        // function by using this navigation property.
        [ForeignKey(nameof(TheCinemaId))]
        public Cinema Cinema { get; set; }
        public HashSet<Movie> Movies { get; set; }
    }
}
