using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderMongoPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoClient>(opts => new MongoClient(new MongoUrl(configuration["Mongo:ConnectionString"])));
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}