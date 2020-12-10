using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Web.Controllers
{
    public class CastController : Controller
    {
        public string Index()
        {
            return "This is cast controller index";
        }
        public string Detail(int castid)
        {
            return "This is cast controller ";
        }
    }
}
