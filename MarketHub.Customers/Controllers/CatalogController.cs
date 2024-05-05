using MarketHub.Customers.Models;
using MarketHub.Domain.Filters;
using MarketHub.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketHub.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogService _catalogService;

        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index(int id = 1)
        {
            var response = await _catalogService.GetCatalog(id);
            ViewBag.Id = id;
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return View(response.Data!);
            return RedirectToAction("Error");
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(int id, CatalogFilter filter = null!)
        {
            if(filter == null)
                filter = new CatalogFilter();
            filter.SubcategoryId = id;
            var response = await _catalogService.GetProducts(filter);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return View(response.Data!);
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> GetProducts(CatalogFilter filter)
        {
            //var response = await _catalogService.GetProducts(filter);
            //if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToAction("GetProducts", filter);
            //return RedirectToAction("Error");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
