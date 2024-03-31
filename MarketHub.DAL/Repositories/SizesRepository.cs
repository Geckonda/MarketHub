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
    public class SizesRepository : IBaseRepository<SizeEntity>
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

        public async Task<SizeEntity?> GetOne(int id)
        {
            return await _db.Sizes
                .Include(x => x.Product)
                .FirstOrDefaultAsync();
        }

        public async Task Update(SizeEntity entity)
        {
            await _db.Sizes
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name));
        }
    }
}
