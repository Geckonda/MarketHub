using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class OrderStatusEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<OrderEntity> Orders { get; set; } = new();
    }
}
