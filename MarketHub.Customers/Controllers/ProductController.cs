using MarketHub.Customers.Service.Interfaces;
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
    }
}
