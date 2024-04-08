using MarketHub.Domain.Abstractions.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Customers.Service.Interfaces
{
    public interface IReviewService
    {
        Task<IBaseResponse<bool>> AddReview(int customerId, int productId, string text, int stars);
        Task<IBaseResponse<bool>> DeleteReview(int customerId, int productId);
    }
}
