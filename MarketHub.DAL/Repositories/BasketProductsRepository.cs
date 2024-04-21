using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Repositories
{
	public class BasketProductsRepository : IBaseRepository<BasketEntityProductEntity>
	{
		private readonly MarketHubDbContext _db;
        public BasketProductsRepository(MarketHubDbContext db)
        {
			_db = db;
        }

		public Task Add(BasketEntityProductEntity entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<BasketEntityProductEntity>?> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<BasketEntityProductEntity?> GetOne(int id)
		{
			return await _db.BasketsProducts
				.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}

		public Task Update(BasketEntityProductEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
