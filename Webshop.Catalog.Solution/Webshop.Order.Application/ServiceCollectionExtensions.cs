using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
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
        services.AddScoped<CatalogClient>();

        services.AddPaymentClient();

        return services;
    }
}