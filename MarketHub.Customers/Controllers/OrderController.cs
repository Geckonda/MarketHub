using MarketHub.Customers.Service.Implementations;
using MarketHub.Customers.Service.Interfaces;
using MarketHub.Domain.ViewModels;
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
		[HttpGet]
		public async Task<IActionResult> GetPreorder(int[] productsId)
		{
			var response = await _orderService.GetBasket(productsId);
			if (response.StatusCode == Domain.Enums.StatusCode.Ok)
				return View(response.Data);
			return RedirectToAction("Error");
		}
		[HttpPost]
		public async Task<IActionResult> MakeOrder(OrderViewModel model)
		{
			var response = await _orderService.MakeOrder(GetUserId(),
				model.Adress, model.Phone, model.Sum,
				model.ProductsId, model.SizesId, model.AmountsId, model.basketProductsId);
			if(response.StatusCode == Domain.Enums.StatusCode.Ok)
				return RedirectToAction("OrderMade");
			return RedirectToAction("Error");
		}
		[HttpGet]
		public IActionResult OrderMade()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> GetMyOrders()
		{
			var response = await _orderService.GetCustomerOrders(GetUserId());
			if (response.StatusCode == Domain.Enums.StatusCode.Ok)
				return View(response.Data);
			return RedirectToAction("Error");
		}
	}
}
