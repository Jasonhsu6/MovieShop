using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieShop.Core.Entities
{
    [Table("MovieCast")]
    public class MovieCast
    {
        public int MovieId { get; set; }
        public int CastId { get; set; }

        [MaxLength(45)]
        public string Character { get; set; }

        public Movie Movie { get; set; }
        public Cast Cast { get; set; }
    }
}
