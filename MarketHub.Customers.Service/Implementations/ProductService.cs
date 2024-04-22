using MarketHub.Customers.Service.Interfaces;
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
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<ProductEntity> _productRepository;
        private readonly IBaseRepository<SizeEntity> _sizeRepository;
        private readonly IBaseRepository<CustomerEntity> _customerRepository;
        public ProductService(IBaseRepository<ProductEntity> productRepository,
            IBaseRepository<SizeEntity> sizeRepository,
            IBaseRepository<CustomerEntity> customerRepository)
        {
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _customerRepository = customerRepository;
        }
        private ProductViewModel GetModelFromEntity(ProductEntity entity)
        {
            return new ProductViewModel
            {
                Id = entity.Id,
                SellerId = entity.SellerId,
                SubcategoryId = entity.SubcategoryId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Amount = entity.Amount,
                Color = entity.Color,
                Img = entity.Img,
                Subcategory = entity.Subcategory,
                Seller = entity.Seller,
                Orders = entity.Orders,
                Baskets = entity.Baskets,
                Reviews = entity.Reviews
                                .OrderByDescending(x => x.Date)
                                .ToList(),
                Sizes = entity.Sizes,
            };
        }
        public async Task<IBaseResponse<ProductViewModel>> GetProduct(int id)
        {
            var baseResponse = new BaseResponse<ProductViewModel>();
            try
            {
                var entity = await _productRepository.GetOne(id);
                if(entity == null)
                {
                    baseResponse.Description = "Товар не найден";
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                baseResponse.Data = GetModelFromEntity(entity);
                baseResponse.Data.CurrentSize = baseResponse.Data.Sizes.Where(x => x.Amount > 0).FirstOrDefault();
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[ProductService | GetProduct]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось найти товар"
                };
            }
        }

        public async Task<IBaseResponse<ProductViewModel>> GetProductBySize(int id, int sizeId, int? customerId)
        {
            var baseResponse = new BaseResponse<ProductViewModel>();
            try
            {
                var entity = await _productRepository.GetOne(id);
                if (entity == null)
                {
                    baseResponse.Description = "Товар не найден";
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                var size = await _sizeRepository.GetOne(sizeId);
                if (size == null || size.ProductId != id)
                {
                    baseResponse.Description = "Товар не найден";
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                baseResponse.Data = GetModelFromEntity(entity);
                if (customerId != null)
                {
                    var customer = await _customerRepository.GetOne((int)customerId);
                    baseResponse.Data.Customer = customer;
                    baseResponse.Data.CurrentBasket = customer!.Basket!.BasketProducts
                        .Where(p => p.ProductId == id && p.SizeId == sizeId)
                        .FirstOrDefault();
                }
                baseResponse.Data.CurrentSize = size;
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[ProductService | GetProduct]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось найти товар"
                };
            }
        }
    }
}
