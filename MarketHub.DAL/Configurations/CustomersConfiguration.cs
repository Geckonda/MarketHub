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
    public class CustomersConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.HasIndex(c => c.Email)
                .IsUnique();
            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(c => c.Password)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasOne(c => c.Role)
                .WithMany(r => r.Customers)
                .HasForeignKey(c => c.RoleId);
            builder
                .HasOne(c => c.Basket)
                .WithOne(b => b.Customer)
                .HasForeignKey<BasketEntity>(c => c.CustomerId);

            builder
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);
        }
    }
}
