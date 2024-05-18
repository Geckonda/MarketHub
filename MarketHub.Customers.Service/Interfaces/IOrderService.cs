using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Customers.Service.Interfaces
{
	public interface IOrderService
	{
		Task<IBaseResponse<OrderViewModel>> GetBasket(int[] productsId);
		Task<IBaseResponse<bool>> MakeOrder(int customerId, string adress, string phone, int sum,
			int[] productsId, int[] sizesId, int[] amount, int[] basketProductsId);

		Task<IBaseResponse<List<OrderEntity>>> GetCustomerOrders(int customerId);
	}
}
