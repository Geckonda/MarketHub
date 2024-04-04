using MarketHub.Customers.Models;
using MarketHub.Service.Implementations;
using MarketHub.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketHub.Customers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _catalogService.GetCatalog();
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return View(response.Data!);
            return RedirectToAction("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
