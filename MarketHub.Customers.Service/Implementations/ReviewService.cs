using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Response;
using MarketHub.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Customers.Service.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IBaseRepository<ReviewEntity> _reviewRepository;
        public ReviewService(IBaseRepository<ReviewEntity> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<IBaseResponse<bool>> AddReview(int customerId, int productId, string text, int stars)
        {
            var baseRespone = new BaseResponse<bool>();
            try
            {
                var reviews = await _reviewRepository.GetAll();
                var review = reviews.FirstOrDefault(x => x.CustomerId == customerId && x.ProductId == productId);
                if(review != null)
                    await _reviewRepository.Delete(review.Id);
                review = new ReviewEntity
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Text = text,
                    Stars = stars,
                    Date = DateTime.Now.ToUniversalTime(),
                };

                await _reviewRepository.Add(review);
                baseRespone.StatusCode = StatusCode.Ok;
                baseRespone.Data = true;
                return baseRespone;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ReviewService | AddReview]: {ex.Message}",
                    Data = false,
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось добавить отзыв"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteReview(int customerId, int productId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var reviews = await _reviewRepository.GetAll();
                var review = reviews
                    .FirstOrDefault(x => x.CustomerId == customerId
                    && x.ProductId == productId);
                if(review == null)
                {
                    response.Data = false;
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }
                await _reviewRepository.Delete(review.Id);
                response.Data = true;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ReviewService | DeleteReview]: {ex.Message}",
                    Data = false,
                    StatusCode = StatusCode.InternalServerError,
                    ErrorForUser = "Не удалось удалить отзыв"
                };
            }
        }
    }
}
