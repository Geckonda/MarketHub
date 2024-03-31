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
    public class BasketsRepository : IBaseRepository<BasketEntity>
    {
        private readonly MarketHubDbContext _db;
        public BasketsRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(BasketEntity entity)
        {
            await _db.Baskets.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Baskets
                .Where(b => b.Id == id)
                .Include(b => b.Products)
                .Include(b => b.Customer)
                .ExecuteDeleteAsync();
        }

        public async Task<List<BasketEntity>?> GetAll()
        {
            return await _db.Baskets
                .Include(b => b.Products)
                .Include(b => b.Customer)
                .ToListAsync();
        }

        public async Task<BasketEntity?> GetOne(int id)
        {
            return await _db.Baskets
                .Where(b => b.Id == id) 
                .FirstOrDefaultAsync();
        }

        public async Task Update(BasketEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
