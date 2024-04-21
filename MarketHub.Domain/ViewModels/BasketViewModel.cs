using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public CustomerEntity? Customer { get; set; }
        public List<ProductEntity> Products { get; set; } = new();

    }
}
