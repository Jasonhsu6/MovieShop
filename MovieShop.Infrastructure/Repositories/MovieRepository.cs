﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
    public class MovieRepository: EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }
        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies
                                        .Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.MovieGenres)
                                        .ThenInclude(m => m.Genre)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return null;
            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                                              .AverageAsync(r => r == null ? 0 : r.Rating);
            //if (movieRating > 0) movie.Rating = movieRating;
            return movie;
        }
        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue)
                .Take(50).ToListAsync();
            return movies;
        }
        //public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        //{
        //    var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(50).ToListAsync();
        //    return movies;
        //}

        public async Task<IEnumerable<Movie>> GetMovieByGenre(int genreid)
        {
            var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreid).Include(mg => mg.Movie)
                             .Select(m => m.Movie)
                             .ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                                     .GroupBy(r => new
                                     {
                                         Id = r.MovieId,
                                         r.Movie.PosterUrl,
                                         r.Movie.Title,
                                         r.Movie.ReleaseDate
                                     })
                                     .OrderByDescending(g => g.Average(m => m.Rating))
                                     .Select(m => new Movie
                                     {
                                         Id = m.Key.Id,
                                         PosterUrl = m.Key.PosterUrl,
                                         Title = m.Key.Title,
                                         ReleaseDate = m.Key.ReleaseDate,
                                         //Rating = m.Average(x => x.Rating)
                                     })
                                     .Take(20)
                                     .ToListAsync();

            return topRatedMovies;
        }
    }
}
