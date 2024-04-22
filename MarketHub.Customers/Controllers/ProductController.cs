using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.Abstractions.Responses;
using MarketHub.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.Customers.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        private int GetUserId() => Convert.ToInt32(User.FindFirst("userId")!.Value);
        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProduct(id);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
            {
                return View(response!.Data);
            }
            return RedirectToAction("Error");
        }
        [HttpGet]
        public async Task<IActionResult> GetProductBySize(int id, int sizeId)
        {
            IBaseResponse<ProductViewModel> response;
            if (User.Identity!.IsAuthenticated) 
                response = await _productService.GetProductBySize(id, sizeId, GetUserId());
            else
                response = await _productService.GetProductBySize(id, sizeId, null);

            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
            {
                return View("GetProduct", response!.Data);
            }
            return RedirectToAction("Error");
        }
    }
}
