using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
    public class ReviewEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; } = string.Empty;
        public float Stars { get; set; } = 0;
        public DateTime Date { get; set; }

        public CustomerEntity? Customer { get; set; }
        public ProductEntity? Product { get; set; }
    }
}
