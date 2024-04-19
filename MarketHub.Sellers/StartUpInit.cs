using MarketHub.DAL.Repositories;
using MarketHub.Domain.Abstractions.Repositories;
using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Entities;
using MarketHub.Service.Implementations;
using MarketHub.Service.Interfaces;

namespace MarketHub
{
    public static class StartUpInit
    {
        public static void InitialiseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<SellerEntity>, SellersRepository>();
            services.AddScoped<IBaseRepository<CategoryEntity>, CategoriesRepository>();
            services.AddScoped<IBaseRepository<SubcategoryEntity>, SubcategoriesRepository>();
            services.AddScoped<IBaseRepository<ProductEntity>, ProductsRepository>();
            services.AddScoped<ISellerItemRepository<ProductEntity>, ProductsRepository>();
        }
        public static void InitialiseServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
