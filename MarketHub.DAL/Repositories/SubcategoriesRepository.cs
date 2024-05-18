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
    public class SubcategoriesRepository : IBaseRepository<SubcategoryEntity>
    {
        private readonly MarketHubDbContext _db;
        public SubcategoriesRepository(MarketHubDbContext db)
        {
            _db = db;
        }
        public async Task Add(SubcategoryEntity entity)
        {
            await _db.Subcategories.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Subcategories
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<SubcategoryEntity>?> GetAll()
        {
            return await _db.Subcategories
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<SubcategoryEntity?> GetOne(int id)
        {
            return await _db.Subcategories
                .Where(s => s.Id == id)
                .Include(s => s.Products)
                .Include(s => s.Category)
                .FirstOrDefaultAsync();
        }

        public async Task Update(SubcategoryEntity entity)
        {
            await _db.Subcategories
                .Where(s => s.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, entity.Name));
        }
    }
}
