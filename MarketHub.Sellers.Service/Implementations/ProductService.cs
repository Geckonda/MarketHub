using MarketHub.DAL.Repositories;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Response;
using MarketHub.Domain.ViewModels;
using MarketHub.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<ProductEntity> _productsRepository;
        public ProductService(IBaseRepository<ProductEntity> productRepository)
        {
            _productsRepository = productRepository;
        }
        private ProductEntity ModelToEntity(ProductViewModel model)
        {
            return new ProductEntity()
            {
                Id = model.Id,
                SellerId = model.SellerId,
                SubcategoryId = model.SubcategoryId,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Amount = model.Amount,
                Color = model.Color,
                Img = model.Img,
                Sizes = model.Sizes,
            };
        }
        private uint SumAmount(List<SizeEntity> amount)
        {
            uint result = 0;
            foreach (var item in amount)
                result += item.Amount;
            return result;
        }
        public async Task<IBaseResponse<bool>> CreateProduct(ProductViewModel model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                model.Amount = SumAmount(model.Sizes);
                var product = ModelToEntity(model);
                await _productsRepository.Add(product);
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[BookService | CreateBook]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось добавить товар"
                };
            }
        }
    }
}
