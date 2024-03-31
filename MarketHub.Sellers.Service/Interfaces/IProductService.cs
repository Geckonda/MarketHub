using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<bool>> CreateProduct(ProductViewModel model);
    }
}
