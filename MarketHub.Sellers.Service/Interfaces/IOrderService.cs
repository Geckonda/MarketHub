using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Sellers.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<List<OrderEntity>>> GetActiveOrders(int sellerId);
    }
}
