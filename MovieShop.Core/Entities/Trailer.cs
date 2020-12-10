using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Entities
{
    // One movie can have multiple trailers
    public class Trailer
    {
        public int Id { get; set; }
        // Foreign Key from movie table which is id as pk
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        // Navigation Properties, help us navigate to related entities
        // trailerid 24 => get me movie title and movie overview
        public Movie Movie { get; set; }
    }
}
