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
        private readonly IBaseRepository<CategoryEntity> _categoryRepository;
        public CatalogService(IBaseRepository<ProductEntity> productsRepository,
            IBaseRepository<CategoryEntity> categoryRepository)
        {
            _productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IBaseResponse<CatalogViewModel>> GetCatalog()
        {
            var baseResponse = new BaseResponse<CatalogViewModel>();
            try
            {
                var data = new CatalogViewModel();
                var products = await _productsRepository.GetAll();
                var categories = await _categoryRepository.GetAll();
                data.Products = products!.OrderBy(x => Guid.NewGuid()).Take(8).ToList();
                data.Categories = categories!;
                baseResponse.Data = data;
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<CatalogViewModel>()
                {
                    Description = $"[ICatalogService | GetCatalog]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось добавить книгу"
                };
            }
        }
        //private static CatalogViewModel GetModelFromProduct(ProductEntity product)
        //{
        //    return new CatalogViewModel()
        //    {
        //        Id = product.Id,
        //        Seller = product.Seller,
        //        Reviews = product.Reviews,
        //        Subcategory = product.Subcategory,
        //        Color = product.Color,
        //        Sizes = product.Sizes,
        //        Amount = product.Amount,
        //        Img = product.Img,
        //        Description = product.Description,
        //        Price = product.Price,
        //    };
        //}
    }
}
