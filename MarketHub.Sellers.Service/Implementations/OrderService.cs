using MarketHub.Domain.Abstractions.Repositories.Bundle;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Response;
using MarketHub.Sellers.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Sellers.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderBundleRepository _orderRepository;
        public OrderService(IOrderBundleRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IBaseResponse<List<OrderEntity>>> GetActiveOrders(int sellerId)
        {
            var response = new BaseResponse<List<OrderEntity>>();
            try
            {
                response.Data = await _orderRepository.GetAllSellerOrders(sellerId);
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderEntity>>()
                {
                    Description = $"[OrderSerivce | GetActiveOrders]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось получить активные заказы"
                };
            }
        }
    }
}
