﻿using MarketHub.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int SubcategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public uint Amount { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public IFormFile? CoverImage { get; set; }

        public SubcategoryEntity? Subcategory { get; set; }
        public List<SubcategoryEntity>? Subcategories { get; set; }
        public SellerEntity? Seller { get; set; }
        public List<OrderEntity> Orders { get; set; } = new();
        public List<BasketEntity> Baskets { get; set; } = new();
        public List<ReviewEntity> Reviews { get; set; } = new();

        //public List<ColorEntity> Colors { get; set; } = new();
        public List<SizeEntity> Sizes { get; set; } = new();
        public SizeEntity? CurrentSize { get; set; }

        public CustomerEntity? Customer { get; set; }
        public BasketEntityProductEntity? CurrentBasket { get; set; }
    }
}
