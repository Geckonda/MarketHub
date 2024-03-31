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
    public class CategoriesConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder
                .HasMany(c => c.Subcategories)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);

            builder.HasData(
                new CategoryEntity
				{
					Id = 1,
					Name = "Мужская одежда",
                },
                new CategoryEntity
				{
					Id = 2,
					Name = "Женская одежда",
                }
                );
        }
    }
}
