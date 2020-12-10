using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _encryptionService;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IAsyncRepository<Review> _reviewRepository;
        private readonly IMovieService _movieService;
        public UserService(IUserRepository userRepository, ICryptoService cryptoService,
            IAsyncRepository<Favorite> favoriteRepository, IPurchaseRepository purchaseRepository,
            IAsyncRepository<Review> reviewRepository, IMovieService movieService)
        {
            _userRepository = userRepository;
            _encryptionService = cryptoService;
            _favoriteRepository = favoriteRepository;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
            _movieService = movieService;
        }
        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Email Already Exits");

            var salt = _encryptionService.CreateSalt();
            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return response;
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // we are gonna check if the email exists in the database
            var user = await _userRepository.GetUserByEmail(email);
            var hashedPassword = _encryptionService.HashPassword(password, user.Salt);
            var isSuccess = user.HashedPassword == hashedPassword;
            var response = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            //var response = _mapper.Map<UserLoginResponseModel>(user);
            //var userRoles = roles.ToList();
            //if (userRoles.Any())
            //{
            //    response.Roles = userRoles.Select(r => r.Role.Name).ToList();
            //}
            if (isSuccess)
                return response;
            else
                return null;
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var response = new UserRegisterResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return response;
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favoriteMovies = await _favoriteRepository.ListAllWithIncludesAsync(
                p => p.UserId == id,
                p => p.Movie);
            var movies = new List<FavoriteResponseModel.FavoriteMovieResponseModel>();
            foreach (var favorite in favoriteMovies)
            {
                movies.Add(new FavoriteResponseModel.FavoriteMovieResponseModel { 
                    Id = favorite.Movie.Id,
                    Title = favorite.Movie.Title,
                    PosterUrl = favorite.Movie.PosterUrl,
                    //ReleaseDate = favorite.Movie.ReleaseDate
                });
            }
            var response = new FavoriteResponseModel
            {
                UserId = id,
                FavoriteMovies = movies
            };
            return response;
        }
        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            // See if Movie is already Favorite.
            if (await FavoriteExists(favoriteRequest.UserId, favoriteRequest.MovieId))
                throw new Exception("Movie already Favorited");

            var favorite = new Favorite
            {
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId,
            };
            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbFavorite =
                await _favoriteRepository.ListAsync(r => r.UserId == favoriteRequest.UserId &&
                                                         r.MovieId == favoriteRequest.MovieId);
            // var favorite = _mapper.Map<Favorite>(favoriteRequest);
            await _favoriteRepository.DeleteAsync(dbFavorite.First());
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(f => f.MovieId == movieId &&
                                                                 f.UserId == id);
        }


        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.ListAllWithIncludesAsync(
                        p => p.UserId == id,
                        p => p.Movie);
            var movies = new List<PurchasedMovieResponseModel>();
            foreach (var purchase in purchases)
            {
                movies.Add(new PurchasedMovieResponseModel
                {
                    Id = purchase.Movie.Id,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl,
                    //ReleaseDate = favorite.Movie.ReleaseDate
                });
            }
            var response = new PurchaseResponseModel
            {
                UserId = id,
                PurchasedMovies = movies
            };
            return response;
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            if (await IsMoviePurchased(purchaseRequest))
                throw new Exception("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);
            purchaseRequest.TotalPrice = movie.Price;

            var purchase = new Purchase
            {
                UserId = purchaseRequest.UserId,
                MovieId = purchaseRequest.MovieId
            };
            await _purchaseRepository.AddAsync(purchase);
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            return await _purchaseRepository.GetExistsAsync(p =>
                p.UserId == purchaseRequest.UserId && p.MovieId == purchaseRequest.MovieId);
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var reviews = await _reviewRepository.ListAllWithIncludesAsync(
                    p => p.UserId == id,
                    p => p.Movie);
            var movieReviews = new List<ReviewMovieResponseModel>();
            foreach (var review in movieReviews)
            {
                movieReviews.Add(new ReviewMovieResponseModel
                {
                    UserId = id,
                    MovieId = review.MovieId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                    Name = review.Name
                });
            }
            var response = new ReviewResponseModel
            {
                UserId = id,
                MovieReviews = movieReviews
            };
            return response;
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.DeleteAsync(review.First());
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            await _reviewRepository.UpdateAsync(review);
        }
    }
}
