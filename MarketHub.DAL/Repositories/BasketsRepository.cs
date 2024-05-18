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

        public async Task AddProductToBasket(BasketEntityProductEntity bp)
        {
            await _db.BasketsProducts.AddAsync(bp);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Baskets
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();
        }

		public async Task EditBasket(int id, int productCount)
		{
            await _db.BasketsProducts
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.ProductsCount, productCount));
		}

		public async Task<List<BasketEntity>?> GetAll()
        {
            return await _db.Baskets
                .Include(b => b.Products)
                .Include(b => b.Customer)
                .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .ToListAsync();
        }

        public async Task<List<BasketEntity>?> GetAllByCustomerId(int customerId)
        {
            return await _db.Baskets
                .Where(b => b.CustomerId == customerId)
                .Include(b => b.Products)
                .Include(b => b.Customer)
                .Include (b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .ToListAsync();
        }

        public async Task<BasketEntity?> GetOne(int id)
        {
            return await _db.Baskets
                .Where(b => b.Id == id)
                .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .FirstOrDefaultAsync();
        }

        public async Task<BasketEntity?> GetOneByCustomerId(int customerId, int itemId)
        {
            return await _db.Baskets
                .Where (b => b.CustomerId == customerId && b.Id == itemId)
                .Include(b => b.Products)
                .Include(b => b.Customer)
                .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
                .FirstOrDefaultAsync();
        }

        public async Task<BasketEntity?> GetOneByCustomerId(int customerId)
        {
            return await _db.Baskets
               .Where(b => b.CustomerId == customerId)
               .Include(b => b.Products)
               .Include(b => b.Customer)
               .Include(b => b.BasketProducts)
                    .ThenInclude(x => x.Size)
               .FirstOrDefaultAsync();
        }

		public async Task<BasketEntity?> GetBusketWithProducts(int[] productsId)
		{
            var bp = await _db.BasketsProducts
                .Where(x => productsId
					.Contains(x.Id))
                .Include(x => x.Product)    
                .ToListAsync();
            var basket = await _db.Baskets
                .Include(b => b.Customer)
                .FirstAsync();
            basket.BasketProducts = bp;
            basket.Products = bp.Select(x => x.Product)!.ToList()!;

			return basket;
		}

		public async Task<BasketEntityProductEntity> GetOneBySizeId(int basketId, int productId, int sizeId)
		{
            return await _db.BasketsProducts
                .Where(x => x.BasketsId == basketId && x.ProductId == productId && x.SizeId == sizeId)
				.FirstOrDefaultAsync();
		}

		public async Task RemoveProductFromBasket(int basketId, int productId, int sizeId)
        {
            await _db.BasketsProducts
                .Where(x => x.BasketsId == basketId
                && x.ProductId == productId
                && x.SizeId == sizeId)
                .ExecuteDeleteAsync();
            await _db.SaveChangesAsync();
        }

        public async Task RemoveSeveralProductFromBasket(int basketId, int productId, int sizeId, int productCount)
        {
            var basket = await _db.BasketsProducts
                .Where(x => x.BasketsId == basketId
                && x.ProductId == productId
                && x.SizeId == sizeId)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(x => x.ProductsCount, productCount));
        }

        public Task Update(BasketEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
