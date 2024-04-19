using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repositories
{
    public interface ISellerItemRepository<T>
    {
        Task<T?> GetOneBySellerId(int sellerId, int itemId);
        Task<List<T>?> GetAllBySellerId(int sellerId);
    }
}
