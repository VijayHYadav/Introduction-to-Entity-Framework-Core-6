using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Entities
{
    // * [NotMapped]
    [Owned]
    public class Address
    {
        // * public int Id { get; set; } because this is own, we don't really need an ID here because we will use the ID of the main entity
        public string Street { get; set; }
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
    }
}