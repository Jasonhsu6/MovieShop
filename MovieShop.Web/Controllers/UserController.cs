using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Web.Controllers
{
    public class UserController : Controller
    {
        public string Index()
        {
            return "This is user controller index";
        }
        //public IActionResult Create(User user)
        //{
        //    return View();
        //}
        public string Detail(int userid)
        {
            return "This is user controller detail";
        }
    }
}
