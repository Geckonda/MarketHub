using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Repositories;

namespace MarketHub.DAL.Repositories
{
    public class SizesRepository : IBaseRepository<SizeEntity>,
        ISizesRepository,
        ISellerItemRepository<SizeEntity>
    {
        private readonly MarketHubDbContext _db;
        public SizesRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(SizeEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Sizes
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<SizeEntity>?> GetAll()
        {
            return await _db.Sizes
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<List<SizeEntity>?> GetAllBySellerAndProductId(int sellerId, int productId)
        {
            return await _db.Sizes
                .Where(s => s.ProductId == productId
                && s.Product!.SellerId == sellerId)
                .ToListAsync();
        }

        public async Task<List<SizeEntity>?> GetAllBySellerId(int sellerId)
        {
            return await _db.Sizes
                .Where(s => s.Product!.SellerId == sellerId)
                .ToListAsync();
        }

        public async Task<SizeEntity?> GetOne(int id)
        {
            return await _db.Sizes
                .Where(x => x.Id == id)
                .Include(x => x.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<SizeEntity?> GetOneBySellerId(int sellerId, int itemId)
        {
            return await _db.Sizes
                .Where(s => s.Id == itemId
                && s.Product!.SellerId == sellerId)
                .FirstOrDefaultAsync();
        }

        public async Task Update(SizeEntity entity)
        {
            await _db.Sizes
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Amount, entity.Amount));
        }
    }
}
