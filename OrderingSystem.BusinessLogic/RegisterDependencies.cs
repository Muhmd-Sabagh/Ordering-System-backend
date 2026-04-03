using Microsoft.Extensions.DependencyInjection;
using OrderingSystem.BusinessLogic.MapperConfigration;
using OrderingSystem.BusinessLogic.Services;
using OrderingSystem.Global.Interfaces.Services;

namespace OrderingSystem.BusinessLogic
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
