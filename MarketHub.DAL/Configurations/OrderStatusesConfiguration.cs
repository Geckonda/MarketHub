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
    public class OrderStatusesConfiguration : IEntityTypeConfiguration<OrderStatusEntity>
    {
        public void Configure(EntityTypeBuilder<OrderStatusEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasMany(x => x.Orders)
                .WithOne(x => x.OrderStatus)
                .HasForeignKey(x => x.StatusId);
			builder.HasData(
				new OrderStatusEntity
				{
					Id = 1,
					Name = "Новый",
				},
				new OrderStatusEntity
				{
					Id = 2,
					Name = "Отклонен",
				},
				new OrderStatusEntity
				{
					Id = 3,
					Name = "В пути",
				},
				new OrderStatusEntity
				{
					Id = 4,
					Name = "Доставлен",
				}
				);
		}
    }
}
