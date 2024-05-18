using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Repositories.Bundle;

namespace MarketHub.DAL.Repositories
{
    public class OrdersRepository : IOrderBundleRepository
    {

        private readonly MarketHubDbContext _db;
        public OrdersRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(OrderEntity entity)
        {
            await _db.Orders.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Orders
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<OrderEntity>?> GetAll()
        {
            return await _db.Orders
                .Include(x => x.Products)
                .Include(x => x.Customer)
                .Include(x => x.OrderStatus)
                .ToListAsync();
        }

		public async Task<List<OrderEntity>?> GetAllCustomerOrders(int customerId)
		{

            return await _db.Orders
                .Where(x => x.CustomerId == customerId)
				.Include(x => x.OrdersProducts)
                    .ThenInclude(x => x.Product)
				.Include(x => x.Customer)
				.Include(x => x.OrderStatus)
                .ToListAsync();
		}

		public async Task<OrderEntity?> GetLastCustomerOrder(int customerId)
		{
            return await _db.Orders
                .OrderByDescending(x => x.Id)
				.Include(x => x.Products)
				.Include(x => x.Customer)
				.Include(x => x.OrderStatus)
				.FirstOrDefaultAsync(x => x.CustomerId == customerId);
		}

		public async Task<OrderEntity?> GetOne(int id)
        {
            return await _db.Orders
                .Where(x => x.Id == id)
                .Include(x => x.Products)
                .Include(x => x.Customer)
                .Include(x => x.OrderStatus)
                .FirstOrDefaultAsync();
        }

        public async Task Update(OrderEntity entity)
        {
            await _db.Orders
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Adress, entity.Adress)
                    .SetProperty(p => p.OrderDate, entity.OrderDate)
                    .SetProperty(p => p.DeliveryDate, entity.DeliveryDate)
                    .SetProperty(p => p.ShelfLife, entity.ShelfLife)
                    .SetProperty(p => p.Sum, entity.Sum));
        }
    }
}
