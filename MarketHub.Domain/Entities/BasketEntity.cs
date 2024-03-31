using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class BasketEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerEntity? Customer { get; set; }
        public List<ProductEntity> Products { get; set; } = new();
    }
}
