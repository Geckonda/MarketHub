using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Response;
using MarketHub.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<CategoryEntity> _categoriesRepository;
        public CategoryService(IBaseRepository<CategoryEntity> categoryRepository)
        {
            _categoriesRepository = categoryRepository;
        }
        public async Task<IBaseResponse<List<CategoryEntity>>> GetCategories()
        {
            var baseResponse = new BaseResponse<List<CategoryEntity>>();
            try
            {
                baseResponse.Data = await _categoriesRepository.GetAll();
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CategoryEntity>>()
                {
                    Description = $"[CategoryService | GetCategories]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить категории"
                };
            }
        }
    }
}
