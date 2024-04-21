using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketHub.Domain.Abstractions.Repositories;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Repositories.Bundle;

namespace MarketHub.DAL.Repositories
{
    public class BasketsRepository : IBaseRepository<BasketEntity>,
        ICustomerItemRepository<BasketEntity>,
        IBasketBundleRepository
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

        public async Task AddProductToBasket(int customerId, int productId)
        {
            var basket = await _db.Baskets.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
            var product = await _db.Products.Where(x => x.Id ==  productId).FirstOrDefaultAsync();
            basket!.Products.Add(product!);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Baskets
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<BasketEntity>?> GetAll()
        {
            return await _db.Baskets
                .Include(b => b.Products)
                .Include(b => b.Customer)
                .ToListAsync();
        }

        public async Task<List<BasketEntity>?> GetAllByCustomerId(int customerId)
        {
            return await _db.Baskets
                .Where(b => b.CustomerId == customerId)
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

        public async Task<BasketEntity?> GetOneByCustomerId(int customerId, int itemId)
        {
            return await _db.Baskets
                .Where (b => b.CustomerId == customerId && b.Id == itemId)
                .Include(b => b.Products)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync();
        }

        public async Task<BasketEntity?> GetOneByCustomerId(int customerId)
        {
            return await _db.Baskets
               .Where(b => b.CustomerId == customerId)
               .Include(b => b.Products)
               .Include(b => b.Customer)
               .FirstOrDefaultAsync();
        }

        public async Task RemoveProductFromBasket(int customerId, int productId)
        {
            var basket = await _db.Baskets.Where(x => x.CustomerId == customerId)
                .Include (b => b.Products)
                .FirstOrDefaultAsync();
            var product = await _db.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            basket!.Products.Remove(product!);
            await _db.SaveChangesAsync();
        }

        public Task Update(BasketEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
