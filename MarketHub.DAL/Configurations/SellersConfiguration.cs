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
    public class SellersConfiguration : IEntityTypeConfiguration<SellerEntity>
    {
        public void Configure(EntityTypeBuilder<SellerEntity> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.HasIndex(s => s.Email)
                .IsUnique();
            builder.Property(s => s.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(s => s.Password)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasOne(s => s.Role)
                .WithMany(r => r.Sellers)
                .HasForeignKey(s => s.RoleId);

            builder
                .HasMany(s => s.Products)
                .WithOne(p => p.Seller)
                .HasForeignKey(p => p.SellerId);
        }
    }
}
