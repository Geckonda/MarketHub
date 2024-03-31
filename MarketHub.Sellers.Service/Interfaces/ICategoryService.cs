using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IBaseResponse<List<CategoryEntity>>> GetCategories();
    }
}
