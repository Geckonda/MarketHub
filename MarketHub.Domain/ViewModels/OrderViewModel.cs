using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
	public class OrderViewModel
	{
		public BasketEntity? Basket { get; set; }
		public string Adress { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public int Sum {  get; set; }
		public int[] SizesId { get; set; }
		public int[] ProductsId { get; set; }
		public int[] AmountsId { get; set; }
		public int[] basketProductsId { get; set; }
	}
}
