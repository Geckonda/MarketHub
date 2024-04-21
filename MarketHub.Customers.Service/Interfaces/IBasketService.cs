using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Customers.Service.Interfaces
{
    public interface IBasketService
    {
        //Task <IBaseResponse<bool>> CreateBasket(int customerId);
        Task <IBaseResponse<bool>> DeleteBasket(int basketId, int customerId);
        Task <IBaseResponse<BasketViewModel>> GetBasket(int customerId);
        Task<IBaseResponse<bool>> AddProductToBasket(int customerId, int productId, int sizeId, int productsCount);
        Task<IBaseResponse<bool>> RemoveProductFromBasket(int customerId, int productId);
    }
}
