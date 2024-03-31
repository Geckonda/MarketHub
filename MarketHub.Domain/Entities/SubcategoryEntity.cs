using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class SubcategoryEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public CategoryEntity? Category { get; set; }
        public List<ProductEntity> Products { get; set; } = new();
    }
}
