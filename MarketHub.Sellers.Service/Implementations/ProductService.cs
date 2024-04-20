using MarketHub.DAL.Repositories;
using MarketHub.Domain.Abstractions.Repositories;
using MarketHub.Domain.Abstractions.Repositories.Bundle;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarketHub.Service.Implementations
{
    public class ProductService : IProductService
    {
        //private readonly IBaseRepository<ProductEntity> _productsRepository;
        //private readonly ISellerItemRepository<ProductEntity> _sellerItemRepository;
        //private readonly ISizesRepository _sizesEditRepository;
        //private readonly IBaseRepository<SizeEntity> _sizesRepository;
        //public ProductService(IBaseRepository<ProductEntity> productRepository,
        //    IBaseRepository<SizeEntity> sizesRepository, 
        //    ISellerItemRepository<ProductEntity> sellerItemRepository,
        //    ISizesRepository sizesEditRepository)
        //{
        //    _productsRepository = productRepository;
        //    _sellerItemRepository = sellerItemRepository;
        //    _sizesEditRepository = sizesEditRepository;
        //    _sizesRepository = sizesRepository;        
        //}
        private readonly ISizesRepository _sizesEditRepository;
        private readonly IBaseRepository<SizeEntity> _sizesRepository;
        private readonly IProductBundleRepository _productBundleRepository;
        public ProductService(IProductBundleRepository productBundleRepository,
            IBaseRepository<SizeEntity> sizesRepository,
            ISizesRepository sizesEditRepository)
        {
            _productBundleRepository = productBundleRepository;
            _sizesRepository = sizesRepository;
            _sizesEditRepository = sizesEditRepository;
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
        private ProductEntity ModelToEntity(SellerProductViewModel model)
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
        private SellerProductViewModel EntityToModel(ProductEntity entity)
        {
            return new SellerProductViewModel()
            {
                Id = entity.Id,
                SellerId  = entity.SellerId,
                SubcategoryId = entity.SubcategoryId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Amount = entity.Amount,
                Color = entity.Color,
                Img = entity.Img,
                Sizes = entity.Sizes,
                Orders = entity.Orders,
                Seller = entity.Seller,
                Subcategory = entity.Subcategory,
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
                await _productBundleRepository.Add(product);
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ProductService | CreateProduct]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось добавить товар"
                };
            }
        }

        public async Task<IBaseResponse<List<SellerProductViewModel>>> GetSellerProducts(int sellerId)
        {
            var baseResponse = new BaseResponse<List<SellerProductViewModel>>();
            try
            {
                var entities = await _productBundleRepository.GetAllBySellerId(sellerId);
                if(entities == null)
                {
                    return new BaseResponse<List<SellerProductViewModel>>()
                    {
                        Description = $"[ProductService | GetSellerProducts]: Товары не найдены",
                        StatusCode = StatusCode.NotFound,
                        ErrorForUser = "Товары не найдены"
                    };
                }
                var models = new List<SellerProductViewModel>();
                foreach (var entity in entities)
                {
                    var model = EntityToModel(entity);
                    models.Add(model);
                }
                baseResponse.StatusCode= StatusCode.Ok;
                baseResponse.Data = models;
                baseResponse.Description = $"Было найдено товаров :{models.Count}.";
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SellerProductViewModel>> ()
                {
                    Description = $"[ProductService | GetSellerProducts]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить список товаров"
                };
            }
        }

        public async Task<IBaseResponse<SellerProductViewModel>> GetSellerProduct(int sellerId, int productId)
        {
            var response = new BaseResponse<SellerProductViewModel> ();
            try
            {
                var entity = await _productBundleRepository.GetOneBySellerId(sellerId, productId);
                if(entity == null)
                    return new BaseResponse<SellerProductViewModel>()
                    {
                        Description = $"[ProductService | GetSellerProduct]: Товар не найден",
                        StatusCode = StatusCode.NotFound,
                        ErrorForUser = "Товар не найден"
                    };
                var model = EntityToModel(entity);
                response.Data = model;
                response.StatusCode = StatusCode.Ok;
                response.Description = "Товар найден";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<SellerProductViewModel>()
                {
                    Description = $"[ProductService | GetSellerProduct]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить товар"
                };
            }
        }

        public async Task<IBaseResponse<bool>> EditProduct(SellerProductViewModel model)
        {
            var response = new BaseResponse <bool> ();
            try
            {
                var entity = ModelToEntity(model);
                await _productBundleRepository.Update(entity);
                response.Data = true;
                response.StatusCode = StatusCode.Ok;
                response.Description = "Товар изменен";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ProductService | EditProduct]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось изменить товар",
                    Data = false,
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteProduct(int sellerId, int productId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var entity = await _productBundleRepository.GetOneBySellerId(sellerId, productId);
                if (entity == null)
                    return new BaseResponse<bool>()
                    {
                        Description = $"[ProductService | DeleteProduct]: Товар не найден",
                        StatusCode = StatusCode.NotFound,
                        ErrorForUser = "Товар не найден",
                        Data = false,
                    };
                await _productBundleRepository.Delete(productId);
                response.Data = true;
                response.StatusCode = StatusCode.Ok;
                response.Description = "Товар удален";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ProductService | DeleteProduct]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось удалить товар",
                    Data = false,
                };
            }
        }

        public async Task<IBaseResponse<SizeEditorViewModel>> GetSellerProductSizes(int sellerId, int productId)
        {
            var response = new BaseResponse<SizeEditorViewModel>();
            try
            {
                var sizes = await _sizesEditRepository.GetAllBySellerAndProductId(sellerId, productId);
                var product = await _productBundleRepository.GetOneBySellerId(sellerId, productId);
                if (product == null)
                    return new BaseResponse<SizeEditorViewModel>()
                    {

                        Description = $"Не удалось найти товар",
                        StatusCode = StatusCode.InternalServerError,
                        ErrorForUser = "Не удалось найти товар",
                    };
                var model = new SizeEditorViewModel
                {
                    Sizes = sizes!,
                    Product = product,
                    ProductId = product.Id
                };
                response.Data = model;
                response.StatusCode = StatusCode.Ok;
                response.Description = $"Найден товар в количестве {product.Amount}";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<SizeEditorViewModel>()
                {
                    Description = $"[ProductService | GetSellerProductSizes]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить товары",
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteSizes(int sizeId, int productId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var size = await _sizesRepository.GetOne(sizeId);
                var product = await _productBundleRepository.GetOne(productId);
                if (size == null)
                    return new BaseResponse<bool>()
                    {
                        Description = $"Не удалось найти размер",
                        StatusCode = StatusCode.InternalServerError,
                        ErrorForUser = "Не удалось найти размер",
                        Data = false,
                    }; 
                if (product == null)
                    return new BaseResponse<bool>()
                    {
                        Description = $"Не удалось найти товар",
                        StatusCode = StatusCode.InternalServerError,
                        ErrorForUser = "Не удалось найти товар",
                        Data = false,
                    };
                product.Amount -= size.Amount;
                await _productBundleRepository.EditProductAmount(product.Id, product.Amount);
                await _sizesRepository.Delete(sizeId);
                response.Data = true;
                response.StatusCode = StatusCode.Ok;
                return response;

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ProductService | DeleteSizes]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось удалить размер",
                    Data = false,
                };
            }
        }

		public async Task<IBaseResponse<bool>> CreateSizes(SizeEditorViewModel model)
		{
            var response = new BaseResponse<bool>();
            try
			{
				var product = await _productBundleRepository.GetOne(model.ProductId);
                if (product == null)
				    return new BaseResponse<bool>()
				    {
					    Description = $"Не удалось найти товар",
					    StatusCode = StatusCode.InternalServerError,
					    ErrorForUser = "Не удалось найти товар",
					    Data = false,
				    };
                product.Amount += model.Amount;
				var size = new SizeEntity()
                {
                    Name = model.Name,
                    Amount = model.Amount,
                    ProductId = model.ProductId,
                };
                await _productBundleRepository.EditProductAmount(product.Id, product.Amount);
                await _sizesRepository.Add(size);
				response.Data = true;
				response.StatusCode = StatusCode.Ok;
				return response;
			}
            catch (Exception ex)
            {
				return new BaseResponse<bool>()
				{
					Description = $"[ProductService | CreateSizes]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError,
					ErrorForUser = "Не удалось создать размер",
					Data = false,
				};
			}
		}

		public async Task<IBaseResponse<bool>> AddSizes(SizeEditorViewModel model)
		{
            var response = new BaseResponse<bool>();
            try
			{
				var size = await _sizesRepository.GetOne(model.SizeId);
				var product = await _productBundleRepository.GetOne(model.ProductId);
				if (size == null)
					return new BaseResponse<bool>()
					{
						Description = $"Не удалось найти размер",
						StatusCode = StatusCode.InternalServerError,
						ErrorForUser = "Не удалось найти размер",
						Data = false,
					};
				if (product == null)
				    return new BaseResponse<bool>()
				    {
					    Description = $"Не удалось найти товар",
					    StatusCode = StatusCode.InternalServerError,
					    ErrorForUser = "Не удалось найти товар",
					    Data = false,
				    };
				product.Amount += model.Amount;
                size.Amount += model.Amount;
                await _productBundleRepository.EditProductAmount(product.Id, product.Amount);
                await _sizesRepository.Update(size);
				response.Data = true;
				response.StatusCode = StatusCode.Ok;
				return response;
			}
            catch (Exception ex)
            {
				return new BaseResponse<bool>()
				{
					Description = $"[ProductService | AddSizes]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError,
					ErrorForUser = "Не удалось добавить размер",
					Data = false,
				};
			}
		}
	}
}
