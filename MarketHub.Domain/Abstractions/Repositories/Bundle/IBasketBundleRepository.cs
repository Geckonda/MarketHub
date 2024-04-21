using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repositories.Bundle
{
    public interface IBasketBundleRepository:
        IBaseRepository<BasketEntity>,
        ICustomerItemRepository<BasketEntity>
    {
        public Task<BasketEntity?> GetOneByCustomerId(int customerId);
        public Task AddProductToBasket(int customerId, int productId);
        public Task RemoveProductFromBasket(int customerId, int productId);
    }
}
