using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.jsonRequest;
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
        //public async Task<IActionResult> AddProductToBasket(int productId, int sizeId, int productsCount)
        public async Task<bool> AddProductToBasket([FromBody] BasketInput basketInput)
        {
            var response = await _basketService.AddProductToBasket(GetUserId(), basketInput.productId, basketInput.sizeId, basketInput.productsCount);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return response.Data;
            return response.Data;
        }
		[HttpPost]
        public async Task<IActionResult> FirstAddProductToBasket(int productId, int sizeId, int productsCount)
        {
			var response = await _basketService.AddProductToBasket(GetUserId(), productId, sizeId, productsCount);
			if (response.StatusCode == Domain.Enums.StatusCode.Ok)
				return RedirectToRoute(new { controller = "Product", action = "GetProductBySize", id = productId, sizeId = sizeId });
			return RedirectToAction("Error");
		}
		[HttpPost]
        public async Task<IActionResult> RemoveProductFromBasket(int productId, int sizeId, int productsCount)
        {
            var response = await _basketService.RemoveProductFromBasket(GetUserId(), productId, sizeId, productsCount);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Basket", action = "GetBasket", id = GetUserId() });
            return RedirectToAction("Error");
        }
    }
}
