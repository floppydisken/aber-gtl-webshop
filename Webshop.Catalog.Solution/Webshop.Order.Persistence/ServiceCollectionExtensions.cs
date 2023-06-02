using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderMongoPersistence(this IServiceCollection services)
    {
        services.AddScoped<IMongoClient>(provider => new MongoClient(
            new MongoUrl(provider.GetRequiredService<IConfiguration>()["Mongo:ConnectionString"])));
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}