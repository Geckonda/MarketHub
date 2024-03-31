using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class ColorEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public uint Amount { get; set; }
        public string Img { get; set; } = string.Empty;

        public ProductEntity? Product { get; set; }
    }
}
