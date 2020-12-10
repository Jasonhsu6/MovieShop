using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            // create MovieRepo instance in every method in my service class
            // newing up is very convineint but we need to avoid it as much as we can
            // make sure you dont break any existing code....
            // always go back do the regression testing...
            //  _movieRepository = new MovieRepository(new MovieShopDbContext(null));
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }


        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            var movie = new Movie
            {
                Id = movieCreateRequest.Id,
                Title = movieCreateRequest.Title,
                Overview = movieCreateRequest.Overview,
                Tagline = movieCreateRequest.Tagline,
                Revenue = movieCreateRequest.Revenue,
                Budget = movieCreateRequest.Budget,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                PosterUrl = movieCreateRequest.PosterUrl,
                BackdropUrl = movieCreateRequest.BackdropUrl,
                OriginalLanguage = movieCreateRequest.OriginalLanguage,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                RunTime = movieCreateRequest.RunTime,
                Price = movieCreateRequest.Price,
            };
            await _movieRepository.AddAsync(movie);

            var response = new MovieDetailsResponseModel
            {
                Id = movieCreateRequest.Id,
                Title = movieCreateRequest.Title,
                Overview = movieCreateRequest.Overview,
                Tagline = movieCreateRequest.Tagline,
                Revenue = movieCreateRequest.Revenue,
                Budget = movieCreateRequest.Budget,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                PosterUrl = movieCreateRequest.PosterUrl,
                BackdropUrl = movieCreateRequest.BackdropUrl,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                RunTime = movieCreateRequest.RunTime,
                Price = movieCreateRequest.Price,
                Genres = movieCreateRequest.Genres
            };
            return response;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetHighestRatedMovies()
        {
            var movies = await _movieRepository.GetTopRatedMovies();
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

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
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

        public async Task<IEnumerable<MovieResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.ListAllAsync();
            var response = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                response.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title
                });
            }
            return response;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies()
        {

            // Repository?
            // MovieRepository class
            // var repository = new MovieRepository(new MovieShopDbContext(null));
            var movies = await _movieRepository.GetHighestRevenueMovies();
            // Map our Movie Entity to MovieResponseModel
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                movieResponseModel.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    //ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title
                });
            }
            return movieResponseModel;
        }

        public async Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            var reviews = await _movieRepository.GetMovieReviews(id);
            var response = new List<ReviewMovieResponseModel>();
            foreach (var review in reviews)
            {
                response.Add(new ReviewMovieResponseModel
                {
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    ReviewText = review.ReviewText,
                    Name = review.User.FirstName + " " + review.User.LastName
                });
            }
            return response;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMovieByGenre(genreId);
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

        public async Task<IEnumerable<PurchaseResponseModel>> GetAllMoviePurchases()
        {
            //will return first 30 because of pagination
            var purchases = await _purchaseRepository.GetAllPurchases();
            var response = new List<PurchaseResponseModel>();
            foreach (var purchase in purchases)
            {
                var movies = new List<PurchasedMovieResponseModel>();
                movies.Add(new PurchasedMovieResponseModel
                {
                    Id = purchase.Movie.Id,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl,
                    ReleaseDate = purchase.Movie.ReleaseDate.Value,
                    PurchaseDateTime = purchase.PurchaseDateTime
                });
                response.Add(new PurchaseResponseModel
                {
                    UserId = purchase.UserId,
                    PurchasedMovies = movies
                });
            }
            return response;
        }

        //public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        //{
        //    var movie = new Movie
        //    {
        //    {
        //        Id = movieCreateRequest.Id,
        //        Title = movieCreateRequest.Title,
        //        Overview = movieCreateRequest.Overview,
        //        Tagline = movieCreateRequest.Tagline,
        //        Revenue = movieCreateRequest.Revenue,
        //        Budget = movieCreateRequest.Budget,
        //        ImdbUrl = movieCreateRequest.ImdbUrl,
        //        TmdbUrl = movieCreateRequest.TmdbUrl,
        //        PosterUrl = movieCreateRequest.PosterUrl,
        //        BackdropUrl = movieCreateRequest.BackdropUrl,
        //        OriginalLanguage = movieCreateRequest.OriginalLanguage,
        //        ReleaseDate = movieCreateRequest.ReleaseDate,
        //        RunTime = movieCreateRequest.RunTime,
        //        Price = movieCreateRequest.Price
        //    };
        //    await _movieRepository.UpdateAsync(movie);

        //    var response = new MovieDetailsResponseModel
        //    {
        //        Id = movieCreateRequest.Id,
        //        Title = movieCreateRequest.Title,
        //        Overview = movieCreateRequest.Overview,
        //        Tagline = movieCreateRequest.Tagline,
        //        Revenue = movieCreateRequest.Revenue,
        //        Budget = movieCreateRequest.Budget,
        //        ImdbUrl = movieCreateRequest.ImdbUrl,
        //        TmdbUrl = movieCreateRequest.TmdbUrl,
        //        PosterUrl = movieCreateRequest.PosterUrl,
        //        BackdropUrl = movieCreateRequest.BackdropUrl,
        //        ReleaseDate = movieCreateRequest.ReleaseDate,
        //        RunTime = movieCreateRequest.RunTime,
        //        Price = movieCreateRequest.Price,
        //        Genres = movieCreateRequest.Genres
        //    };
        //    return response;
        //}
    }
}
