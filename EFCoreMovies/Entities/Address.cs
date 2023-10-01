using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.Entities
{
    // * [NotMapped]
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}