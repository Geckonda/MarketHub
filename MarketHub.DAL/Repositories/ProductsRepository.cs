using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Repositories;
using MarketHub.Domain.Abstractions.Repositories.Bundle;

namespace MarketHub.DAL.Repositories
{
    public class ProductsRepository : IProductBundleRepository
    {

        private readonly MarketHubDbContext _db;
        public ProductsRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(ProductEntity entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Products
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task EditProductAmount(int productId, uint amount)
        {
            await _db.Products
                .Where(p => p.Id == productId)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.Amount, amount));
        }

        public async Task<List<ProductEntity>?> GetAll()
        {
            return await _db.Products
                .Include(x => x.Subcategory)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Seller)
                .Include(x => x.Orders)
                .Include(x => x.Baskets)
                .Include(x => x.Reviews)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.Sizes)
                .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .ToListAsync();
        }

        public async Task<List<ProductEntity>?> GetAllBySellerId(int sellerId)
        {
            return await _db.Products
                .Where(x => x.SellerId == sellerId)
                .Include(x => x.Subcategory)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Seller)
                .Include(x => x.Orders)
                .Include(x => x.Baskets)
                .Include(x => x.Reviews)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.Sizes)
                .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .ToListAsync();
        }

        public async Task<ProductEntity?> GetOne(int id)
        {
            return await _db.Products
                .Where (x => x.Id == id)
                .Include(x => x.Subcategory)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Seller)
                .Include(x => x.Orders)
                .Include(x => x.Baskets)
                .Include(x => x.Reviews)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.Sizes)
                .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .FirstOrDefaultAsync();
        }

        public async Task<ProductEntity?> GetOneBySellerId(int sellerId, int itemId)
        {
            return await _db.Products
                .Where(x => x.SellerId == sellerId && x.Id == itemId)
                .Include(x => x.Subcategory)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Seller)
                .Include(x => x.Orders)
                .Include(x => x.Baskets)
                .Include(x => x.Reviews)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.Sizes)
                .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ProductEntity entity)
        {
            await _db.Products
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.SubcategoryId, entity.SubcategoryId)
                    .SetProperty(p => p.Description, entity.Description)
                    .SetProperty(p => p.Price, entity.Price)
                    .SetProperty(p => p.Color, entity.Color)
                    .SetProperty(p => p.Img, entity.Img));
        }
    }
}
