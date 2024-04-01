using MarketHub.Domain.ViewModels;
using MarketHub.Sellers.Models;
using MarketHub.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketHub.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger,
			ICategoryService categoryService)
		{
			_logger = logger;
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Index()
		{
			if(!User.Identity!.IsAuthenticated)
				return RedirectToAction("Login", "Account");
			var response = await _categoryService.GetCategories();
			if(response.StatusCode == Domain.Enums.StatusCode.Ok)
			{
				var model = new IndexViewModel();
				model.Categories = response.Data!; 
                return View(model);
            }
            return RedirectToAction("Error");
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
