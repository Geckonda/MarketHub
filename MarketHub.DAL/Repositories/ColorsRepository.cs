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
    public class ColorsRepository : IBaseRepository<ColorEntity>
    {
        private readonly MarketHubDbContext _db;
        public ColorsRepository(MarketHubDbContext db)
        {
            _db = db;
        }

        public async Task Add(ColorEntity entity)
        {
            await _db.Colors.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Colors
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<ColorEntity>?> GetAll()
        {
            return await _db.Colors
                .Include(c => c.Product)
                .ToListAsync();
        }

        public async Task<ColorEntity?> GetOne(int id)
        {
            return await _db.Colors
                .Include(c => c.Product)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ColorEntity entity)
        {
            await _db.Colors
                .Where(c => c.Id == entity.Id)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Img, entity.Img));
        }
    }
}
