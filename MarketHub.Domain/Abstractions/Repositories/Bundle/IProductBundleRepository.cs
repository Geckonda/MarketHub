﻿using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repositories.Bundle
{
    public interface IProductBundleRepository:
        IBaseRepository<ProductEntity>,
        ISellerItemRepository<ProductEntity>
    {
        Task<uint> GetProductAmount(int productId);
        Task EditProductAmount(int productId, uint amount);
    }
}
