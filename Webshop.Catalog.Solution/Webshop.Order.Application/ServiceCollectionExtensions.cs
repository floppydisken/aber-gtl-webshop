using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webshop.Application;
using Webshop.Order.Persistence;
using Webshop.Payment.Client;

namespace Webshop.Order.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddOrderMongoPersistence();
        services.AddHttpClient();
        services.AddScoped<CatalogClient>(provider => new CatalogClient(provider.GetRequiredService<HttpClient>(), new() {
            Uri = new Uri(configuration.GetValue<string>("Catalog:Uri"))
        }));
        services.AddScoped<CustomerClient>(provider => new CustomerClient(provider.GetRequiredService<HttpClient>(), new()));
        services.AddPaymentClient();
        services.AddDispatcher(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}