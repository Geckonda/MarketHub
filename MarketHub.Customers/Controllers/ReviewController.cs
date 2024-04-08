using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.jsonRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.Customers.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> AddReview([FromBody]ReviewInput review)
        {
            var response = await _reviewService
                .AddReview(review.customerId,
                review.productId,
                review.text,
                review.stars);

            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return true;
            return false;
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> DeleteReview([FromBody] ReviewInput review)
        {
            var response = await _reviewService
                .DeleteReview(review.customerId,
                review.productId);

            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return response.Data;
            return response.Data;
        }
    }
}
