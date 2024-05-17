using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Configurations
{
	public class OrderEntityProductEntityConfiguration : IEntityTypeConfiguration<OrderEntityProductEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntityProductEntity> builder)
		{

			builder.HasKey(p => p.Id);

			builder.Property(p => p.ProductName)
				.HasColumnType("varchar(100)")
				.IsRequired();
			builder.Property(p => p.SellerName)
				.HasColumnType("varchar(100)")
				.IsRequired();
			builder.Property(p => p.SizeName)
				.HasColumnType("varchar(100)")
				.IsRequired();
			builder.Property(p => p.Price)
				.IsRequired();
			builder.Property(p => p.Amount)
				.HasColumnType("int")
				.IsRequired();

		}
	}
}
