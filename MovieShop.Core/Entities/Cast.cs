using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieShop.Core.Entities
{
    [Table("Cast")]
    public class Cast
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(4096)]
        public string Name { get; set; }
        [MaxLength(4096)]
        public string Gender { get; set; }
        [MaxLength(4096)]
        public string TmdbUrl { get; set; }
        [MaxLength(2084)]
        public string ProfilePath { get; set; }
        public ICollection<MovieCast> MovieCasts { get; set; }
    }
}
