using MarketHub.Customers.Service.Implementations;
using MarketHub.Customers.Service.Interfaces;
using MarketHub.DAL.Repositories;
using MarketHub.Domain.Abstractions.Repositories;
using MarketHub.Domain.Abstractions.Repositories.Bundle;
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
            services.AddScoped<IBaseRepository<CustomerEntity>, CustomersRepository>();
            services.AddScoped<IBaseRepository<ProductEntity>, ProductsRepository>();
            services.AddScoped<IBaseRepository<CategoryEntity>, CategoriesRepository>();
            services.AddScoped<IBaseRepository<SubcategoryEntity>, SubcategoriesRepository>();
            services.AddScoped<IBaseRepository<ReviewEntity>, ReviewsRepository>();
            services.AddScoped<IBaseRepository<BasketEntity>, BasketsRepository>();
            services.AddScoped<IBaseRepository<CustomerEntity>, CustomersRepository>();
            services.AddScoped<IBaseRepository<BasketEntityProductEntity>, BasketProductsRepository>();
            services.AddScoped<IBaseRepository<SizeEntity>, SizesRepository>();
            services.AddScoped<ICustomerItemRepository<BasketEntity>, BasketsRepository>();

            //bundle
            services.AddScoped<IBasketBundleRepository, BasketsRepository>();

        }
        public static void InitialiseServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
