using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Entities
{
	public class OrderEntityProductEntity
	{
		public int Id { get; set; }
		public int OrderId {  get; set; }
		public OrderEntity? Order { get; set; }
		public int ProductId { get; set; }
		public ProductEntity? Product { get; set; }
		public int SellerId { get; set; }
		public string SellerName { get; set; } = string.Empty;
		public int SubcategoryId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public decimal Price { get; set; } = decimal.Zero;
		public uint Amount { get; set; }
		public string Color { get; set; } = string.Empty;
		public string Img { get; set; } = string.Empty;
		public int SizeId { get; set; }
		public string SizeName { get; set; } = string.Empty;
	}
}
