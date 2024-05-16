using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.Abstractions.Repositories.Bundle;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Response;
using MarketHub.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Customers.Service.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IBasketBundleRepository _basketRepository;
		private readonly IBaseRepository<BasketEntityProductEntity> _basketProductsRepository;
		private readonly IBaseRepository<SizeEntity> _sizeRepository;

		public OrderService(IBasketBundleRepository basketRepository,
			IBaseRepository<SizeEntity> sizeRepository,
			IBaseRepository<BasketEntityProductEntity> basketProductRepository)
		{
			_basketRepository = basketRepository;
			_sizeRepository = sizeRepository;
			_basketProductsRepository = basketProductRepository;

		}
        public async Task<IBaseResponse<OrderViewModel>> GetBasket(int[] productsId)
		{
			var response = new BaseResponse<OrderViewModel>();
			try
			{
				var entity = await _basketRepository.GetBusketWithProducts(productsId);
				var model = GetBasketViewModelFromEntity(entity!);
				response.Data = model;
				response.StatusCode = StatusCode.Ok;
				response.Description = "Корзина найдена";
				return response;
			}
			catch (Exception ex)
			{
				return new BaseResponse<OrderViewModel>()
				{
					Description = $"[OrderService | GetBasket]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError,
					ErrorForUser = "Не удалось получить сформированный заказ"
				};
			}
		}
		private OrderViewModel GetBasketViewModelFromEntity(BasketEntity entity)
		{
			return new OrderViewModel()
			{
				Basket = entity,
				//Id = entity.Id,
				//Customer = entity.Customer,
				//Products = entity.Products,
				//BasketProducts = entity.BasketProducts,
			};
		}
	}
}
