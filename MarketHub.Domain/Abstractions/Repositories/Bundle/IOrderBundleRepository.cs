using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repositories.Bundle
{
	public interface IOrderBundleRepository :
		IBaseRepository<OrderEntity>
	{
		Task<OrderEntity?> GetLastCustomerOrder(int customerId);
		Task<List<OrderEntity>?> GetAllCustomerOrders(int customerId);
		Task<List<OrderEntity>?> GetAllSellerOrders(int sellerId);
	}
}
