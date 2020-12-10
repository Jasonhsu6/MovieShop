using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IMovieService _movieService;
        public AdminController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> AddMovie(MovieCreateRequest movieRequestModel)
        {
            var movie = await _movieService.CreateMovie(movieRequestModel);
            if (movie == null)
            {
                return BadRequest("Created failed");
            }
            return Ok(movie);
        }
        [HttpPut("movie")]
        public async Task<IActionResult> UpdateMovie( MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.UpdateMovie(movieCreateRequest);
            return Ok(createdMovie);
        }
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _movieService.GetAllMoviePurchases();
            if (!purchases.Any())
            {
                return NotFound("No purchases found");
            }
            return Ok(purchases);
        }
        [HttpGet]
        [Route("top")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            // call our service and call the method
            // var movies = _movieService.GetTopMovies();
            // http status code
            var movies = await _movieService.GetTopRevenueMovies();
            if (!movies.Any())
            {
                return NotFound("No movies found");
            }
            return Ok(movies);
        }
    }
}
