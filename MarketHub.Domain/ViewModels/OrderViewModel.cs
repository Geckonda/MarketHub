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
		public string Adress = string.Empty;
		public string Phone = string.Empty;
		public int[] SizesId { get; set; }
		public int[] ProductsId { get; set; }
	}
}
