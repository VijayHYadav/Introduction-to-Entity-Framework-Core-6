using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.Entities
{
    public class RentableMovie :  Product
    {
        public int MovieId { get; set; }
    }
}