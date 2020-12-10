using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(UserRegisterRequestModel userRegisterRequest)
        {
            if (ModelState.IsValid)
            {
                return Ok(userRegisterRequest);
            }
            return BadRequest(new { message = "please correct the input information" });
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserDetail(int id)
        {
            var user = await _userService.GetUserDetails(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequestModel loginRequest)
        {
            if (ModelState.IsValid)
            {
                return Ok(loginRequest);
            }
            return BadRequest(new { message = "Login failed" });
        }

    }
}
