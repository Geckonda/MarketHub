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
    public class CustomersRepository : IBaseRepository<CustomerEntity>
    {
        private readonly MarketHubDbContext _db;
        public CustomersRepository(MarketHubDbContext db)
        {
            _db = db;
        }
        public async Task Add(CustomerEntity entity)
        {
            await _db.Customers.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Customers
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<CustomerEntity>?> GetAll()
        {
            return await _db.Customers
                .Include(x => x.Role)
                .Include(x => x.Basket)
                .Include(x => x.Reviews)
                .Include(x => x.Orders)
                .ToListAsync();
        }

        public async Task<CustomerEntity?> GetOne(int id)
        {
            return await _db.Customers
                .Where(x => x.Id == id) 
                .Include(x => x.Role)
                .Include(x => x.Basket)
                .Include(x => x.Reviews)
                .Include(x => x.Orders)
                .FirstOrDefaultAsync();
        }

        public async Task Update(CustomerEntity entity)
        {
            await _db.Customers
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Email, entity.Email)
                    .SetProperty(p => p.Password, entity.Password));
        }
    }
}
