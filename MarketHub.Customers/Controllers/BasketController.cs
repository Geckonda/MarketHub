using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.Customers.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        private int GetUserId() => Convert.ToInt32(User.FindFirst("userId")!.Value);
        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var response = await _basketService.GetBasket(GetUserId());
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return View(response.Data);
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(int productId, int sizeId, int productsCount)
        {
            var response = await _basketService.AddProductToBasket(GetUserId(), productId, sizeId, productsCount);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Product", action = "GetProductBySize", id = productId, sizeId = sizeId });
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveProductFromBasket(int productId)
        {
            var response = await _basketService.RemoveProductFromBasket(GetUserId(), productId);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Basket", action = "GetBasket", id = GetUserId() });
            return RedirectToAction("Error");
        }
    }
}
