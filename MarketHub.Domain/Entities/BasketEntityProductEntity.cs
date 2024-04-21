using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class BasketEntityProductEntity
    {
        public int Id { get; set; }

        public int BasketsId { get; set; }
        public BasketEntity? Basket { get; set; }

        public int ProductId { get; set; }
        public ProductEntity? Product { get; set; }
        public int SizeId { get; set; }
        public SizeEntity? Size { get; set; }
        public int ProductsCount { get; set;}
        
    }
}
