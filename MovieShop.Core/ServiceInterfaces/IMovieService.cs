using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Helpers;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
        Task<IEnumerable<MovieResponseModel>> GetHighestRatedMovies();
        Task<IEnumerable<MovieResponseModel>> GetAllMovies();
        Task<IEnumerable<PurchaseResponseModel>> GetAllMoviePurchases();
        //Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId);

        Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id);
        //Task<int> GetMoviesCount(string title = "");
        //Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies();
        Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId);
        Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest);
        //Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest);
    }
}
