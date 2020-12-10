using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    // attribute based routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        //api/movies/toprevenue
        [HttpGet]
        [Route("toprevenue")]
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
        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetHighestRatedMovies();
            if (!movies.Any())
            {
                return NotFound("No movies found");
            }
            return Ok(movies);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound("No movie found");
            }
            return Ok(movie);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if (!movies.Any())
            {
                return NotFound("No movies found");
            }
            return Ok(movies);
        }
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetReviewsForMovie(id);
            if (!reviews.Any())
            {
                return NotFound("No reviews found");
            }
            return Ok(reviews);
        }
        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetGenreById(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);
            if (!movies.Any())
            {
                return NotFound("No movies found");
            }
            return Ok(movies);
        }

    }
}
