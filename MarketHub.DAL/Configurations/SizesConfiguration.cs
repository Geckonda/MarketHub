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
    public class SizesConfiguration : IEntityTypeConfiguration<SizeEntity>
    {
        public void Configure(EntityTypeBuilder<SizeEntity> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasOne(s => s.Product)
                .WithMany(p => p.Sizes)
                .HasForeignKey(s => s.ProductId);
        }
    }
}
