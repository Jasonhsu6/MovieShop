using System;
using System.Collections.Generic;
using System.Text;
using MovieShop.Core.Entities;

namespace MovieShop.Core.Models.Request
{
    public class ReviewRequestModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
        public virtual User User { get; set; }
        public Movie Movie { get; set; }
    }
}
