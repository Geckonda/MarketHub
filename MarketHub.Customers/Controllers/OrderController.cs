using MarketHub.Customers.Service.Implementations;
using MarketHub.Customers.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.Customers.Controllers
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
		public async Task<IActionResult> GetPreorder(int[] productsId)
		{
			var response = await _orderService.GetBasket(productsId);
			if (response.StatusCode == Domain.Enums.StatusCode.Ok)
				return View(response.Data);
			return RedirectToAction("Error");
		}
	}
}
