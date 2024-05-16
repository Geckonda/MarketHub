using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public string Adress {  get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ShelfLife { get; set; }
        public decimal Sum {  get; set; } = decimal.Zero;

        public CustomerEntity? Customer { get; set; }
        public OrderStatusEntity? OrderStatus { get; set; }
        public List<ProductEntity> Products { get; set; } = new();

    }
}
