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
    public class OrderStatusesRepository : IBaseRepository<OrderStatusEntity>
    {

        private readonly MarketHubDbContext _db;
        public OrderStatusesRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(OrderStatusEntity entity)
        {
            await _db.OrderStatuses.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.OrderStatuses
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<OrderStatusEntity>?> GetAll()
        {
            return await _db.OrderStatuses
                .Include(x => x.Orders)
                .ToListAsync();
        }

        public async Task<OrderStatusEntity?> GetOne(int id)
        {
            return await _db.OrderStatuses
                .Include (x => x.Orders)
                .FirstOrDefaultAsync();
        }

        public async Task Update(OrderStatusEntity entity)
        {
            await _db.OrderStatuses
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name));
        }
    }
}
