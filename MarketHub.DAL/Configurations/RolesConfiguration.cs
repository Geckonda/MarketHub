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
    public class RolesConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Name)
                .IsUnique();
            builder.Property(r => r.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasMany(r => r.Customers)
                .WithOne(c => c.Role)
                .HasForeignKey(c => c.RoleId);

            builder
                .HasMany(r => r.Sellers)
                .WithOne(s => s.Role)
                .HasForeignKey(s => s.RoleId);

            builder.HasData(
               new RoleEntity
               {
                   Id = 1,
                   Name = "Seller",
               },
               new RoleEntity
               {
                   Id = 2,
                   Name = "Customer",
               });
        }
    }
}
