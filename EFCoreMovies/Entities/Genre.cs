using System.ComponentModel.DataAnnotations;

namespace EFCoreMovies.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        //[StringLength(maximumLength: 150)]
        //[Required]
        public string Name { get; set; }
    }
}