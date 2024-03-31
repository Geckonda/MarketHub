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
    public class SellersRepository : IBaseRepository<SellerEntity>
    {
        private readonly MarketHubDbContext _db;
        public SellersRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(SellerEntity entity)
        {
            await _db.Sellers.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Sellers
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<SellerEntity>?> GetAll()
        {
            return await _db.Sellers
                .Include(x => x.Role)
                .Include(x => x.Products)
                .ToListAsync();
        }

        public async Task<SellerEntity?> GetOne(int id)
        {
            return await _db.Sellers
                .Where(x => x.Id == id)
                .Include(x => x.Role)
                .Include(x => x.Products)
                .FirstOrDefaultAsync();

        }

        public async Task Update(SellerEntity entity)
        {
            await _db.Sellers
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Email, entity.Email)
                    .SetProperty(p => p.Password, entity.Password));
        }
    }
}
