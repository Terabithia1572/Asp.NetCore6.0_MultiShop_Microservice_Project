using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Services
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Uygulama katmanındaki servislerin DI konteynerine eklenmesi için kullanılır.
            // Burada, uygulama katmanındaki tüm servisler eklenebilir.
            // Örneğin:
            // services.AddScoped<IOrderingService, OrderingService>();
            // services.AddScoped<IProductService, ProductService>();
            // services.AddScoped<ICustomerService, CustomerService>();

            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly)); // MediatR kütüphanesini kullanarak, uygulama katmanındaki tüm komut ve sorgu handler'larını kaydeder.
            // MediatR, CQRS (Command Query Responsibility Segregation) mimarisini uygulamak için kullanılır.
            // Bu sayede, uygulama katmanındaki komut ve sorguların handler'ları otomatik olarak DI konteynerine eklenir.

        }
    }
}
