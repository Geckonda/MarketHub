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
	public class OrdersProductsRepository : IBaseRepository<OrderEntityProductEntity>
	{
		private readonly MarketHubDbContext _db;
		public OrdersProductsRepository(MarketHubDbContext db)
		{
			_db = db;
		}
        public async Task Add(OrderEntityProductEntity entity)
		{
			await _db.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<OrderEntityProductEntity>?> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<OrderEntityProductEntity?> GetOne(int id)
		{
			return _db.OrdersProducts
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task Update(OrderEntityProductEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
