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
    public class ProductsRepository : IBaseRepository<ProductEntity>
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
                //.Include(x => x.Colors)
                .Include(x => x.Sizes)
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
                //.Include(x => x.Colors)
                .Include(x => x.Sizes)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ProductEntity entity)
        {
            await _db.Products
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Description, entity.Description)
                    .SetProperty(p => p.Price, entity.Price)
                    .SetProperty(p => p.Amount, entity.Amount)
                    .SetProperty(p => p.Color, entity.Color)
                    .SetProperty(p => p.Img, entity.Img));
        }
    }
}
