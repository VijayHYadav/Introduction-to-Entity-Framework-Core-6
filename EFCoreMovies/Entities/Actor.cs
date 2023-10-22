﻿using System.ComponentModel.DataAnnotations;

namespace EFCoreMovies.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PictureURL { get; set; }
        public HashSet<MovieActor> MoviesActors { get; set; }
        public Address BillingAddress { get; set; }
        public Address HomeAddress { get; set; }

    }
}
