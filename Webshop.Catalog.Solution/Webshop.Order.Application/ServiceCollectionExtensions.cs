using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Webshop.Order.Persistence;
using Webshop.Payment.Client;

namespace Webshop.Order.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderServices(this IServiceCollection services)
    {
        services.AddMediatR(opts => opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddOrderMongoPersistence();

        services.AddHttpClient();
        services.AddScoped<CatalogClient>(provider =>
        {
            return new CatalogClient(provider.GetRequiredService<HttpClient>(), new());
        });
        services.AddScoped<CustomerClient>(provider =>
        {
            return new CustomerClient(provider.GetRequiredService<HttpClient>(), new());
        });
        services.AddPaymentClient();

        return services;
    }
}