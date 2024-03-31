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
    internal class RoleRepository : IBaseRepository<RoleEntity>
    {

        private readonly MarketHubDbContext _db;
        public RoleRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(RoleEntity entity)
        {
            await _db.Roles.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Roles
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<RoleEntity>?> GetAll()
        {
            return await _db.Roles
                .Include(x => x.Sellers)
                .Include(x => x.Customers)
                .ToListAsync();
        }

        public async Task<RoleEntity?> GetOne(int id)
        {
            return await _db.Roles
                .Include(x => x.Sellers)
                .Include(x => x.Customers)
                .FirstOrDefaultAsync();
        }

        public async Task Update(RoleEntity entity)
        {
            await _db.Roles
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name));
        }
    }
}
