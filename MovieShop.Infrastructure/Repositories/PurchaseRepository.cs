using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }
        public async Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 0)
        {
            var purchases = await _dbContext.Purchases.Include(p => p.Movie).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 0)
        {
            var purchases = await _dbContext.Purchases.Where(p => p.MovieId == movieId).Include(p => p.Movie).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return purchases;
        }
    }
}
