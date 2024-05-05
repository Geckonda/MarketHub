using MarketHub.Domain.Entities;
using MarketHub.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
    public class CatalogViewModel
    {
        public List<ProductEntity> Products { get; set; } = new();
        public CategoryEntity? Category { get; set; }
        public SubcategoryEntity? Subcategory { get; set; }
        public List<string> Colors { get; set; } = new();
        public CatalogFilter Filters { get; set; } = new();
    }
}
