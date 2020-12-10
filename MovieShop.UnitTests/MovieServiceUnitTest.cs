using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Services;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        private MovieService _sut;

        private List<Movie> _movies;

        private Mock<IMovieRepository> _mockMovieRepository;

        [TestInitialize]
        // [OneTimeSetup] in nUnit
        public void OneTimeSetup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            // SUT System under Test MovieService => GetTopRevenueMovies
            _sut = new MovieService(_mockMovieRepository.Object);
            _mockMovieRepository.Setup(m => m.GetHighestRevenueMovies()).ReturnsAsync(_movies);
        }

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            var _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
            };
        }

        [TestMethod]
        public async Task TestListOfHighestGrossingMoviesFromFakeData()
        {
            // Arrange
            // mock objects, data, methods etc
            //  _sut = new MovieService(new MockMovieRepository());
            // Act
            var movies = await _sut.GetTopRevenueMovies();
            // check the actual output with expected data.
            // AAA
            // Arrange, Act and Assert
            // Assert
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieResponseModel>));
            Assert.AreEqual(16, movies.Count());
        }
    }

    //public class MockMovieRepository : IMovieRepository
    //{
    //    public Task<Movie> AddAsync(Movie entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task DeleteAsync(Movie entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Movie> GetByIdAsync(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> GetCountAsync(Expression<Func<Movie, bool>> filter = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> GetExistsAsync(Expression<Func<Movie, bool>> filter = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
    //    {
    //        // this method will get the fake data
    //        var _movies = new List<Movie>
    //        {
    //            new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
    //            new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
    //            new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
    //            new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
    //            new Movie {Id = 5, Title = "Inception", Budget = 1200000},
    //            new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
    //            new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
    //            new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
    //            new Movie
    //            {
    //                Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000
    //            },
    //            new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
    //            new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
    //            new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
    //            new Movie
    //            {
    //                Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000
    //            },
    //            new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
    //            new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
    //            new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
    //        };
    //        return await Task.FromResult(_movies);
    //    }

    //    public Task<IEnumerable<Movie>> GetMovieByGenre(int genreid)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<Movie>> GetTopRatedMovies()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<Movie>> ListAllAsync()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Movie> UpdateAsync(Movie entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
