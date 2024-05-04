using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.Abstractions.Repositories;
using MarketHub.Domain.Abstractions.Repositories.Bundle;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Response;
using MarketHub.Domain.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Customers.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBasketBundleRepository _basketRepository;
        private readonly IBaseRepository<BasketEntityProductEntity> _basketProductsRepository;
        private readonly IBaseRepository<SizeEntity> _sizeRepository;

        public BasketService(IBasketBundleRepository basketRepository,
            IBaseRepository<SizeEntity> sizeRepository,
            IBaseRepository<BasketEntityProductEntity> basketProductRepository)
        {
            _basketRepository = basketRepository;
            _sizeRepository = sizeRepository;
            _basketProductsRepository = basketProductRepository;

        }
        private async Task<bool> CreateBasket(int customerId)
        {
            var basket = new BasketEntity() { CustomerId = customerId };
            try
            {
               await _basketRepository.Add(basket);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<IBaseResponse<bool>> DeleteBasket(int basketId, int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<BasketViewModel>> GetBasket(int customerId)
        {
            var response = new BaseResponse<BasketViewModel>();
            try
            {
                var entity = await _basketRepository.GetOneByCustomerId(customerId);
                if(entity == null)
                {
                    await CreateBasket(customerId);
                    entity = await _basketRepository.GetOneByCustomerId(customerId);
                }
                var model = GetBasketViewModelFromEntity(entity!);
                response.Data = model;
                response.StatusCode = StatusCode.Ok;
                response.Description = "Корзина найдена";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<BasketViewModel>()
                {
                    Description = $"[BasketService | GetBasket]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось найти корзину"
                };
            }
        }

        private BasketViewModel GetBasketViewModelFromEntity(BasketEntity entity)
        {
            return new BasketViewModel()
            {
                Id = entity.Id,
                Customer = entity.Customer,
                Products = entity.Products,
                BasketProducts = entity.BasketProducts,
            };
        }

        public async Task<IBaseResponse<bool>> AddProductToBasket(int customerId, int productId, int sizeId, int productsCount)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var entity = await _basketRepository.GetOneByCustomerId(customerId);
                if (entity == null)
                {
                    await CreateBasket(customerId);
                    entity = await _basketRepository.GetOneByCustomerId(customerId);
                }
                var basket = await _basketRepository.GetOneBySizeId(entity!.Id, productId, sizeId);
                if(basket == null)
				{
					var bp = new BasketEntityProductEntity()
					{
						BasketsId = entity!.Id,
						ProductId = productId,
						SizeId = sizeId,
						ProductsCount = productsCount,
					};
					await _basketRepository.AddProductToBasket(bp);
				}
                else if(productsCount == 0)
                {
                    await _basketRepository.RemoveProductFromBasket(entity!.Id, productId, sizeId);
                }
                else
                {
                    await _basketRepository.EditBasket(basket.Id, productsCount);
                }
                response.Data = true;
                response.StatusCode = StatusCode.Ok;
                response.Description = "Товар добавлен в корзину";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[BasketService | AddProductToBasket]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось добавить товар в корзину",
                    Data = false,
                };
            }
        }

        public async Task<IBaseResponse<bool>> RemoveProductFromBasket(int customerId, int productId, int sizeId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var basket = await _basketRepository.GetOneByCustomerId(customerId);
                await _basketRepository.RemoveProductFromBasket(basket!.Id, productId, sizeId);
                response.Data = true;
                response.StatusCode = StatusCode.Ok;
                response.Description = "Товар убран из корзины";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[BasketService | RemoveProductFromBasket]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось убрать товар из корзины",
                    Data = false,
                };
            }
        }
    }
}
