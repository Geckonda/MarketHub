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
        Task<IBaseResponse<List<SellerProductViewModel>>> GetSellerProducts(int sellerId);
        Task<IBaseResponse<SellerProductViewModel>> GetSellerProduct(int sellerId, int productId);
        Task<IBaseResponse<bool>> EditProduct(SellerProductViewModel model);
        Task<IBaseResponse<bool>> DeleteProduct(int sellerId, int productId);
    }
}
