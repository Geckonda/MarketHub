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
    public class CatalogService : ICatalogService
    {
        private readonly IBaseRepository<ProductEntity> _productsRepository;
        public CatalogService(IBaseRepository<ProductEntity> productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<IBaseResponse<List<CatalogViewModel>>> GetCatalog()
        {
            var baseResponse = new BaseResponse<List<CatalogViewModel>>();
            try
            {
                var products = _productsRepository.GetAll().Result!.Take(4);
                var data = new List<CatalogViewModel>();
                foreach (var product in products)
                {
                    var model = GetModelFromProduct(product);
                    data.Add(model);
                }
                baseResponse.Data = data;
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CatalogViewModel>>()
                {
                    Description = $"[BookService | CreateBook]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось добавить книгу"
                };
            }
        }
        private static CatalogViewModel GetModelFromProduct(ProductEntity product)
        {
            return new CatalogViewModel()
            {
                Id = product.Id,
                Seller = product.Seller,
                Reviews = product.Reviews,
                Subcategory = product.Subcategory,
                Color = product.Color,
                Sizes = product.Sizes,
                Amount = product.Amount,
                Img = product.Img,
                Description = product.Description,
                Price = product.Price,
            };
        }
    }
}
