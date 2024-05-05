using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Filters;
using MarketHub.Domain.Response;
using MarketHub.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Service.Interfaces
{
    public interface ICatalogService
    {
        Task<IBaseResponse<HomeViewModel>> GetHome();
        Task<IBaseResponse<CategoryViewModel>> GetCatalog(int categoryId);
        Task<IBaseResponse<CatalogViewModel>> GetProducts(CatalogFilter filter);
        
    }
}
