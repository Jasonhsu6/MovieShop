using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
        //Task<IEnumerable<MovieResponseModel>> GetHighestRatedMovies();
        //Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "");
        //Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0);
        //Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId);

        //Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id);
        //Task<int> GetMoviesCount(string title = "");
        //Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies();
        //Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId);
        //Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest);
        //Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest);
    }
}
