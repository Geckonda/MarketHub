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
    public class SubcategoriesConfiguration : IEntityTypeConfiguration<SubcategoryEntity>
    {
        public void Configure(EntityTypeBuilder<SubcategoryEntity> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Name)
                .IsUnique();
            builder.Property(s => s.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasOne(s => s.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.CategoryId);
            builder
                .HasMany(s => s.Products)
                .WithOne(p => p.Subcategory)
                .HasForeignKey(p => p.SubcategoryId);

			builder.HasData(
				new SubcategoryEntity
				{
					Id = 1,
                    CategoryId = 1,
					Name = "Толстовки, свитшоты и худи",
				},
				new SubcategoryEntity
				{
					Id = 2,
					CategoryId = 1,
					Name = "Рубашки",
				},
				new SubcategoryEntity
				{
					Id = 3,
					CategoryId = 2,
					Name = "Платья и сарафаны",
				},
				new SubcategoryEntity
				{
					Id = 4,
					CategoryId = 2,
					Name = "Футболки и топы",
				}
				);
		}
    }
}
