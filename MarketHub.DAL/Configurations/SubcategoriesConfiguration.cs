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
					CategoryId = 1,
					Name = "Майки и футболки",
				},
				new SubcategoryEntity
				{
					Id = 4,
					CategoryId = 1,
					Name = "Джинсы и брюки",
				},
				new SubcategoryEntity
				{
					Id = 5,
					CategoryId = 1,
					Name = "Носки",
				},


				new SubcategoryEntity
				{
					Id = 6,
					CategoryId = 2,
					Name = "Платья и сарафаны",
				},
				new SubcategoryEntity
				{
					Id = 7,
					CategoryId = 2,
					Name = "Футболки и топы",
				},
				new SubcategoryEntity
				{
					Id = 8,
					CategoryId = 2,
					Name = "Блузки и рубашки",
				},
				new SubcategoryEntity
				{
					Id = 9,
					CategoryId = 2,
					Name = "Брюки и джинсы",
				},
				new SubcategoryEntity
				{
					Id = 10,
					CategoryId = 2,
					Name = "Толстовки, свитшоты и худи",
				},

				new SubcategoryEntity
				{
					Id = 11,
					CategoryId = 3,
					Name = "Платья и сарафаны",
				},
				new SubcategoryEntity
				{
					Id = 12,
					CategoryId = 3,
					Name = "Блузки и рубашки",
				},
				new SubcategoryEntity
				{
					Id = 13,
					CategoryId = 3,
					Name = "Штанишки",
				},
				new SubcategoryEntity
				{
					Id = 14,
					CategoryId = 3,
					Name = "Футболки и маечки",
				},
				new SubcategoryEntity
				{
					Id = 15,
					CategoryId = 3,
					Name = "Носки",
				}
				);
		}
    }
}
