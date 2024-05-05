using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Filters
{
    public class CatalogFilter
    {
        public int SubcategoryId { get; set; }
        public string? Color { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
        public uint StartPrice { get; set; }
        public uint EndPrice { get; set; }

    }
}
