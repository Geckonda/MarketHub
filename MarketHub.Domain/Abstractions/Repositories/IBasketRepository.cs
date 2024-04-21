using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repositories
{
    public interface IBasketRepository
    {
        Task<BasketEntity> GetOneBySellerId(int sellerId, int itemId);
    }
}
