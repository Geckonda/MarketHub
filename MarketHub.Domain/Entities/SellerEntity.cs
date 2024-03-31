using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class SellerEntity : UserEntity
    {
        public List<ProductEntity> Products { get; set; } = new();
    }
}
