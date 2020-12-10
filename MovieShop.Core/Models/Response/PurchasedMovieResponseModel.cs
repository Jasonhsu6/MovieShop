using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Response
{
    public class PurchasedMovieResponseModel : MovieResponseModel
    {
        public DateTime PurchaseDateTime { get; set; }
    }
}
