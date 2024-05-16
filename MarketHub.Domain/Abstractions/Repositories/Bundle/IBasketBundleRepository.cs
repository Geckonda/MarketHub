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
        public Task<BasketEntity?> GetBusketWithProducts(int[] productsId);
        public Task<BasketEntityProductEntity> GetOneBySizeId(int basketId, int productId, int sizeId);
        public Task EditBasket(int id, int productCount);
        public Task AddProductToBasket(BasketEntityProductEntity bp);
        public Task RemoveProductFromBasket(int basketId, int productId, int sizeId);
        public Task RemoveSeveralProductFromBasket(int basketId, int productId, int sizeId, int productCount);
    }
}
