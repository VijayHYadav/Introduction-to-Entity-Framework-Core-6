﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PictureURL { get; set; }
        public HashSet<MovieActor> MoviesActors { get; set; }
        public Address Address { get; set; }

    }
}
