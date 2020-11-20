using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Web.Controllers
{
    public class MovieController : Controller
    {
        public string Index()
        {
            //return top 20 movies
            return "This is movie controller index";
        }
        public string Details(int movieid)
        {
            return "This is movie controller detail";
        }
        public string MovieByGenre(int genreid)
        {
            return "This is movie controller by genre";
        }
    }
}
