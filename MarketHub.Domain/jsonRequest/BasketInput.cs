using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.jsonRequest
{
	public class BasketInput
	{
		public int productId {  get; set; }
		public int sizeId {  get; set; }
		public int productsCount {  get; set; }
	}
}
