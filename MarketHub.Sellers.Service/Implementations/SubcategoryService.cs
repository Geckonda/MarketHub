using MarketHub.DAL.Repositories;
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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IBaseRepository<SubcategoryEntity >_subcategoriesRepository;
        public SubcategoryService(IBaseRepository<SubcategoryEntity> subcategoryRepository)
        {
            _subcategoriesRepository = subcategoryRepository;
        }
        public async Task<IBaseResponse<List<SubcategoryEntity>>> GetSubcategories(int category)
        {
            var baseResponse = new BaseResponse<List<SubcategoryEntity>>();
            try
            {
                var data = await _subcategoriesRepository.GetAll();
                baseResponse.Data = data.Where(x => x.CategoryId == category).ToList();
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SubcategoryEntity>> ()
                {
                    Description = $"[SubcategoryService | GetSubcategory]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить подкатегории"
                };
            }
        }
    }
}
