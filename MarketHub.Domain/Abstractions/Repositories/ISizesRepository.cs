using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repositories
{
    public interface ISizesRepository
    {
        Task<List<SizeEntity>?> GetAllBySellerAndProductId(int sellerId, int productId);
    }
}
