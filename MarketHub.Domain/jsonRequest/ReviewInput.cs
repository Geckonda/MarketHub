using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.jsonRequest
{
    public class ReviewInput
    {
        public int customerId { get; set; }
        public int productId { get; set; }
        public string text { get; set; }
        public int stars { get; set; }
    }
}
