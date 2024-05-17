using MarketHub.DAL.Configurations;
using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL
{
    public class MarketHubDbContext : DbContext
    {
        public MarketHubDbContext(DbContextOptions<MarketHubDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<BasketEntity> Baskets => Set<BasketEntity>();
        public DbSet<BasketEntityProductEntity> BasketsProducts => Set<BasketEntityProductEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<ColorEntity> Colors => Set<ColorEntity>();
        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<OrderStatusEntity> OrderStatuses => Set<OrderStatusEntity>();
        public DbSet<OrderEntityProductEntity> OrdersProducts => Set<OrderEntityProductEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<SellerEntity> Sellers => Set<SellerEntity>();
        public DbSet<SizeEntity> Sizes => Set<SizeEntity>();
        public DbSet<SubcategoryEntity> Subcategories => Set<SubcategoryEntity>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BasketsConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
            //modelBuilder.ApplyConfiguration(new ColorsConfiguration());
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusesConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewsConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new SellersConfiguration());
            modelBuilder.ApplyConfiguration(new SizesConfiguration());
            modelBuilder.ApplyConfiguration(new SubcategoriesConfiguration());
            base.OnModelCreating(modelBuilder);
		}

    }
}
