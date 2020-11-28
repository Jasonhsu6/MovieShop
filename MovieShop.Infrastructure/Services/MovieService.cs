using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Models;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _repository;
        public MovieService(IMovieRepository repository)
        {
            // create MovieRepo instance in every method in my service class
            // newing up is very convineint but we need to avoid it as much as we can
            // make sure you dont break any existing code....
            // always go back do the regression testing...
            //  _repository = new MovieRepository(new MovieShopDbContext(null));
            _repository = repository;
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _repository.GetByIdAsync(id);
            if (movie == null) throw new Exception("Movie not found");
            var movieResponseModel = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                PosterUrl = movie.PosterUrl,
                ReleaseDate = movie.ReleaseDate.Value,
                Title = movie.Title,
                Overview = movie.Overview,
                Price = movie.Price,
            };
            return movieResponseModel;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies()
        {

            // Repository?
            // MovieRepository class
            // var repository = new MovieRepository(new MovieShopDbContext(null));
            var movies = await _repository.GetHighestRevenueMovies();
            // Map our Movie Entity to MovieResponseModel
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                movieResponseModel.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title
                });
            }
            return movieResponseModel;
        }

    }
}
