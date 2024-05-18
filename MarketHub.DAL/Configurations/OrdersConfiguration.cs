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
    public class OrdersConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(or => or.Id);
            builder.Property(or => or.Adress)
                .HasColumnType("varchar(255)")
                .IsRequired();
            builder.Property(or => or.Phone)
                .HasColumnType("varchar(11)")
                .IsRequired();
            builder.Property(or => or.Sum)
                .HasColumnType("money");
            builder.Property(or => or.OrderDate)
                .IsRequired();
			builder.Property(or => or.DeliveryDate)
				.HasDefaultValue(DateTime.MinValue);
			builder.Property(or => or.ShelfLife)
				.HasDefaultValue(DateTime.MinValue);


			builder
                .HasOne(or => or.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(or => or.CustomerId);

            builder
                .HasOne(or => or.OrderStatus)
                .WithMany(s => s.Orders)
                .HasForeignKey(or => or.StatusId);
            builder
                .HasMany(or => or.Products)
                .WithMany(p => p.Orders);
        }
    }
}
