using MarketHub.Sellers.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.Sellers.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        private int GetUserId() => Convert.ToInt32(User.FindFirst("userId")!.Value);
        public async Task<IActionResult> GetActiveOrders()
        {
            var response = await _orderService.GetActiveOrders(GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return View(response.Data);
            return RedirectToAction("Error");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
