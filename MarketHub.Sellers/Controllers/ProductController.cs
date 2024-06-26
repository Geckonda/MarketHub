﻿using MarketHub.Domain.Entities;
using MarketHub.Domain.Helpers;
using MarketHub.Domain.ViewModels;
using MarketHub.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService productService,
            IWebHostEnvironment webHostEnvironment,
            ISubcategoryService subcategoryService)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _subcategoryService = subcategoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]   
        public async Task<IActionResult> CreateProduct(int category)
        {
            var response = await _subcategoryService.GetSubcategories(category);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
            {
                var model = new ProductViewModel();
                model.Subcategories = response.Data;
                return View(model);
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel model, IFormFile image,  string[] sizeNames, int[] sizeAmount)
        {
            model.SellerId = Convert.ToInt32(User.FindFirst("userId")!.Value);
            model.Img = await SaveImage(model.CoverImage);
            model.Sizes = await CollectSizes(sizeNames, sizeAmount);
            var response = await _productService.CreateProduct(model);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            return RedirectToAction("Error");
        }
        [HttpGet]
        public async Task<IActionResult> GetSellerProducts()
        {
            //var sellerId = Convert.ToInt32(User.FindFirst("userId")!.Value);
            var response = await _productService.GetSellerProducts(GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return View(response.Data);
            return RedirectToAction("Error");
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var productRespnose = await _productService.GetSellerProduct(GetUserId(), id);
            if (productRespnose.StatusCode == Domain.Enums.StatusCode.Ok)
            {
                var subcategoryResponse = await _subcategoryService
                    .GetSubcategories(productRespnose.Data!.Subcategory!.CategoryId);
                if(subcategoryResponse.StatusCode == Domain.Enums.StatusCode.Ok)
                {
                    productRespnose.Data.Subcategories = subcategoryResponse.Data;
                    return View(productRespnose.Data);
                }
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(SellerProductViewModel model)
        {
            if(model.CoverImage != null)
                model.Img = await SaveImage(model.CoverImage);

            var response = await _productService.EditProduct(model);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            return RedirectToAction("Error");

        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(GetUserId(), id);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            return RedirectToAction("Error");

        }
        [HttpGet]
        public async Task<IActionResult> EditProductSizes(int id)
        {
            var response = await _productService.GetSellerProductSizes(GetUserId(), id);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return View(response.Data);
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSizes(SizeEditorViewModel model)
        {
            var response = await _productService.DeleteSizes(model.SizeId, model.ProductId);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Product", action = "EditProductSizes", id=model.ProductId });
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> CreateSizes(SizeEditorViewModel model)
        {
            var response = await _productService.CreateSizes(model);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return RedirectToRoute(new { controller = "Product", action = "EditProductSizes", id = model.ProductId });
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<IActionResult> AddSizes(SizeEditorViewModel model)
        {
			var response = await _productService.AddSizes(model);
			if (response.StatusCode == Domain.Enums.StatusCode.Ok)
				return RedirectToRoute(new { controller = "Product", action = "EditProductSizes", id = model.ProductId });
			return RedirectToAction("Error");
		}
        private int GetUserId() => Convert.ToInt32(User.FindFirst("userId")!.Value);
        private async Task<List<SizeEntity>> CollectSizes(string[] sizeNames, int[] sizeAmount)
        {
            var list = new List<SizeEntity>();
            for (int i = 0; i < sizeNames.Length; i++)
            {
                var entity = new SizeEntity();
                entity.Name = sizeNames[i];
                entity.Amount = (uint)sizeAmount[i];
                list.Add(entity);
            }
            return list;
        }

        private async Task<string> SaveImage(IFormFile? fileImg)
        {
            ImageHelper imageHelper = new(_webHostEnvironment.WebRootPath, "Products");
            if (fileImg != null)
            {
                //if (model.CoverImage != null)
                //{
                //    await imageHelper.DeletePreviousImage(model.Img);
                //}
                string path = await imageHelper.SaveImage(fileImg!);
                //return "https://owa.market-hub.ru" + path;
                return path;
            }
            return string.Empty;
        }

        private async Task DeleteImage(string path)
        {
            ImageHelper imageHelper = new(_webHostEnvironment.WebRootPath, "BookCovers");
            await imageHelper.DeletePreviousImage(path);
        }
    }
}
