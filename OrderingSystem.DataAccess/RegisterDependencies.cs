using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingSystem.DataAccess.Data;
using OrderingSystem.Global.Interfaces.Repositories;
using OrderingSystem.Global.Interfaces.UnitOfWork;

namespace OrderingSystem.DataAccess
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(Repositories.GenericRepository<>));
            services.AddScoped<ICustomerRepository, Repositories.CustomerRepository>();
            services.AddScoped<IOrderRepository, Repositories.OrderRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }
    }
}
