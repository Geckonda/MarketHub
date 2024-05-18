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
		private readonly IBaseRepository<OrderEntityProductEntity> _orderProductsRepository;
		private readonly IProductBundleRepository _productRepository;
		private readonly IOrderBundleRepository _orderRepository;

		public OrderService(IBasketBundleRepository basketRepository,
			IBaseRepository<SizeEntity> sizeRepository,
			IBaseRepository<BasketEntityProductEntity> basketProductRepository,
			IProductBundleRepository productRepository,
			IOrderBundleRepository orderRepository,
			IBaseRepository<OrderEntityProductEntity> orderProductsRepository)
		{
			_basketRepository = basketRepository;
			_sizeRepository = sizeRepository;
			_basketProductsRepository = basketProductRepository;
			_productRepository = productRepository;
			_orderRepository = orderRepository;
			_orderProductsRepository = orderProductsRepository;
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

		public async Task<IBaseResponse<List<OrderEntity>>> GetCustomerOrders(int customerId)
		{
			var response = new BaseResponse<List<OrderEntity>>();
			try
			{
				response.Data = await _orderRepository.GetAllCustomerOrders(customerId);
				response.StatusCode = StatusCode.Ok;
				return response;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<OrderEntity>>()
				{
					Description = $"[OrderService | GetCustomerOrders]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError,
					ErrorForUser = "Не удалось получить бывшие заказы"
				};
			}
		}

		public async Task<IBaseResponse<bool>> MakeOrder(int customerId, string adress, string phone, int sum,
			int[] productsId, int[] sizesId, int[] amount, int[] basketProductsId)
		{
			var response = new BaseResponse<bool>();
			try
			{
				var order = BuildOrderEntity(customerId, adress, phone, sum);
				await _orderRepository.Add(order);
				order = await _orderRepository.GetLastCustomerOrder(customerId);
				for (int i = 0; i < sizesId.Length; i++)
				{
					var product = await _productRepository.GetOne(productsId[i]);
					if(product == null)
						return new BaseResponse<bool>();
					var size = product.Sizes.First(x => x.Id == sizesId[i]);
					var potentialAmount = (int)size!.Amount - amount[i];
					if (potentialAmount >= 0)
					{
						size.Amount = (uint)potentialAmount;
						await _sizeRepository.Update(size);
						await _productRepository.EditProductAmount(productsId[i],
							product!.Amount - (uint)amount[i]);
						var orderProduct = BuildOrederProductEntity(order!, product, size, (uint)amount[i]);
						await _orderProductsRepository.Add(orderProduct);
						await _basketProductsRepository.Delete(basketProductsId[i]);
					}
					else
					{
						response.ErrorForUser += $"Недостаточно товара на складе. Товар:{product.Name}.\n";
					}
				}
				response.Data = true;
				response.StatusCode = StatusCode.Ok;
				return response;

			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[OrderService | MakeOrder]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError,
					ErrorForUser = "Не удалось сформировать заказ"
				};
			}
		}
		private OrderEntity BuildOrderEntity(int customerId, string adress,
			string phone, int sum)
		{
			return new OrderEntity()
			{
				CustomerId = customerId,
				Adress = adress,
				Phone = phone,
				Sum = sum,
				OrderDate = DateTime.UtcNow,
				StatusId = 1
			};
		}
		private OrderEntityProductEntity BuildOrederProductEntity(OrderEntity order,
			ProductEntity product, SizeEntity size, uint amount)
		{
			return new OrderEntityProductEntity()
			{
				OrderId = order.Id,
				ProductId = product.Id,
				SellerId = product.SellerId,
				SellerName = product.Seller!.Name,
				SubcategoryId = product.SubcategoryId,
				ProductName = product.Name,
				Price = product.Price,
				Amount = amount,
				Color = product.Color,
				Img = product.Img,
				SizeId = size.Id,
				SizeName = size.Name,
			};
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
