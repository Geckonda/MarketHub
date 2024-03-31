using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketHub.Domain.Abstractions.Repository;

namespace MarketHub.DAL.Repositories
{
    public class ReviewsRepository : IBaseRepository<ReviewEntity>
    {
        private readonly MarketHubDbContext _db;

        public ReviewsRepository(MarketHubDbContext db)
        {
            _db = db;   
        }
        public async Task Add(ReviewEntity entity)
        {
            await _db.Reviews.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Reviews
                .Where(r => r.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<ReviewEntity>?> GetAll()
        {
            return await _db.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .ToListAsync();
        }

        public async Task<ReviewEntity?> GetOne(int id)
        {
            return await _db.Reviews
                .Where(r => r.Id == id)
                .Include(r => r.Customer)
                .Include (r => r.Product)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ReviewEntity entity)
        {
            await _db.Reviews
                .Where(r => r.Id  == entity.Id)
                .ExecuteUpdateAsync(r => r
                    .SetProperty(p => p.Text, entity.Text)
                    .SetProperty(p => p.Stars, entity.Stars)
                    .SetProperty(p => p.Date, entity.Date));
        }
    }
}
