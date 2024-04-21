using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repositories
{
    public interface ICustomerItemRepository<T>
    {
        Task<T?> GetOneByCustomerId(int customerId, int itemId);
        Task<List<T>?> GetAllByCustomerId(int customerId);
    }
}
