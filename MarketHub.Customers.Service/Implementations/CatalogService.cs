using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Filters;
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
        private readonly IBaseRepository<SubcategoryEntity> _subcategoryRepository;
        public CatalogService(IBaseRepository<ProductEntity> productsRepository,
            IBaseRepository<CategoryEntity> categoryRepository,
            IBaseRepository<SubcategoryEntity> subcategoryRepository)
        {
            _productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<IBaseResponse<CategoryViewModel>> GetCatalog(int categoryId)
        {
            var response = new BaseResponse<CategoryViewModel>();
            try
            {
                var data = new CategoryViewModel();
                data.Categories = await _categoryRepository.GetAll();
                data.Subcategories = data.Categories!.Where(x => x.Id == categoryId).First().Subcategories!;
                response.Data = data;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryViewModel>()
                {
                    Description = $"[CatalogService | GetCatalog]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не получить каталог"
                };
            }
        }

        public async Task<IBaseResponse<HomeViewModel>> GetHome()
        {
            var baseResponse = new BaseResponse<HomeViewModel>();
            try
            {
                var data = new HomeViewModel();
                var products = await _productsRepository.GetAll();
                var categories = _categoryRepository.GetAll().Result!.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                var listA = products!.Where(x => x.Subcategory!.CategoryId == categories!.First().Id).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                var listB = products!.Where(x => x.Subcategory!.CategoryId == categories!.Last().Id).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                data.Products = listA.Union(listB).ToList();

                //data.Products = products!.OrderBy(x => Guid.NewGuid()).Take(8).ToList();
                data.Categories = categories!;
                baseResponse.Data = data;
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<HomeViewModel>()
                {
                    Description = $"[CatalogService | GetHome]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить главную страницу"
                };
            }
        }

        public async Task<IBaseResponse<CatalogViewModel>> GetProducts(CatalogFilter filter)
        {
            var response = new BaseResponse<CatalogViewModel>();
            try
            {
                var products = await _productsRepository.GetAll();
                if(products == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }
                response.Data = new();
                if (filter.SubcategoryId > 0)
                    products = products.Where(x => x.SubcategoryId == filter.SubcategoryId).ToList();

                response.Data.Colors = products.Select(x => x.Color).Distinct().Order().ToList();

                if (filter.ProductName != "" && filter.ProductName != null)
                    products = products.Where(x => x.Name.ToLower().StartsWith(filter.ProductName.ToLower()!)).ToList();
                if (filter.Color != "" && filter.Color!= null)
                    products = products.Where(x => x.Color == filter.Color).ToList();
                if(filter.StartPrice > 0)
                    products = products.Where(x => x.Price >= filter.StartPrice).ToList();
                if (filter.EndPrice > 0)
                    products = products.Where(x => x.Price <= filter.EndPrice).ToList();
                response.Data!.Products = products;
                response.Data.Subcategory = await _subcategoryRepository.GetOne(filter.SubcategoryId);
                response.Data.Category = response.Data.Subcategory!.Category;
                response.Data!.Filters = filter;
                response.StatusCode = StatusCode.Ok;
                response.Description = $"Найдено {products.Count} товаров";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<CatalogViewModel>()
                {
                    Description = $"[CatalogService | GetProducts]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить товары"
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
