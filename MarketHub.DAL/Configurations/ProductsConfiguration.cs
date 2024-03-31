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
    public class ProductsConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(p => p.Description)
                .HasColumnType("text")
                .IsRequired();
            builder.Property(p => p.Price)
                .IsRequired();
            builder.Property(p => p.Amount)
                .HasColumnType("int")
                .IsRequired();

            //Связи
            builder
                .HasOne(p => p.Subcategory)
                .WithMany(sb => sb.Products)
                .HasForeignKey(p => p.SubcategoryId);
            builder
                .HasOne(p => p.Seller)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId);
            builder
                .HasMany(p => p.Orders)
                .WithMany(or => or.Products);
            builder
                .HasMany(p => p.Baskets)
                .WithMany(b => b.Products);
            builder
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);
            //builder
            //    .HasMany(p => p.Colors)
            //    .WithOne(c => c.Product)
            //    .HasForeignKey(c => c.ProductId);
            builder
                .HasMany(p => p.Sizes)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId);
        }
    }
}
