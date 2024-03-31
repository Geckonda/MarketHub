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
    public class ColorsConfiguration : IEntityTypeConfiguration<ColorEntity>
    {
        public void Configure(EntityTypeBuilder<ColorEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            //builder
            //    .HasOne(c => c.Product)
            //    .WithMany(p => p.Colors)
            //    .HasForeignKey(c => c.ProductId);
        }
    }
}
