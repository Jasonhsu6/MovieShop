using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infrastructure.Data
{
    public class MovieShopDbContext: DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }
        //override because it's virtual method
        //Func<int, int string> => string mymethod(int, int), last was output
        //Action(int, int)
        //void mr(int, int)
        // genre, role, cast and crew table are defined with datanotation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);

        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(ur => ur.Role).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.UserId);
            builder.HasOne(ur => ur.User).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.RoleId);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            //ignored columns: DateOfBirth, TwoFactorEnabled, 
            //LockoutEndDate, LastLoginDateTime, IsLocked
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
       
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
            builder.Property(r => r.ReviewText).HasMaxLength(4096);
            builder.HasKey(r => new { r.MovieId, r.UserId });
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            //ignored columns: PurchaseNumber uid
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)");
            builder.HasIndex(p => new { p.MovieId, p.UserId });
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mc => new { mc.MovieId, mc.CrewId, mc.Department, mc.Job});
            builder.Property(mc => mc.Job).HasMaxLength(128);
            builder.Property(mc => mc.Department).HasMaxLength(128);
            builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCrews).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Crew).WithMany(mc => mc.MovieCrews).HasForeignKey(mc => mc.CrewId);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });
            builder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.CastId);
            builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => f.Id);
            builder.HasIndex(f => new { f.MovieId, f.UserId });
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            //Place to configure our Movie entity
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            // check unique index
            //builder.HasIndex(m => m.ImdbUrl).IsUnique();
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            // other properties are not restricted, string is ref type, cannot have ?
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCast { get; set; }

        public DbSet<Crew> Crews { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
