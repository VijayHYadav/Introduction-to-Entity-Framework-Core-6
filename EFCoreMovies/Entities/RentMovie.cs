using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.Entities
{
    public class RentMovie
    {
         public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime EndOfRental { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public Movie Movie { get; set; }
    }
}