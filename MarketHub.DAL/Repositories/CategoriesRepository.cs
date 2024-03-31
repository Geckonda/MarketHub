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
    public class CategoriesRepository : IBaseRepository<CategoryEntity>
    {
        private readonly MarketHubDbContext _db;

        public CategoriesRepository(MarketHubDbContext db)
        {
            _db = db;
        }
        public async Task Add(CategoryEntity entity)
        {
            await _db.Categories.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Categories
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<CategoryEntity>?> GetAll()
        {
            return await _db.Categories.ToListAsync();
        }

        public Task<CategoryEntity?> GetOne(int id)
        {
            return _db.Categories
                .Where(c => c.Id == id)
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync();
        }

        public async Task Update(CategoryEntity entity)
        {
            await _db.Categories
                .Where(c => c.Id ==  entity.Id)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(p => p.Name, entity.Name));
        }
    }
}
