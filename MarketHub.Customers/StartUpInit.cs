using MarketHub.Customers.Service.Implementations;
using MarketHub.Customers.Service.Interfaces;
using MarketHub.DAL.Repositories;
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
            services.AddScoped<IBaseRepository<ReviewEntity>, ReviewsRepository>();

        }
        public static void InitialiseServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
