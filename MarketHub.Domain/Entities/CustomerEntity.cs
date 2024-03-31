using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class CustomerEntity : UserEntity
    {

        public BasketEntity? Basket { get; set; }

        public List<ReviewEntity> Reviews { get; set; } = new();
        public List<OrderEntity> Orders { get; set; } = new();

    }
}
