﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual HashSet<Movie> Movies { get; set; }
    }
}