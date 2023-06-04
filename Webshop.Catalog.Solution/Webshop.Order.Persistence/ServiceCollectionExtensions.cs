using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderMongoPersistence(this IServiceCollection services)
    {
        services.AddScoped<IMongoClient>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var connString = config["Mongo:ConnectionString"];
            return new MongoClient(new MongoUrl(connString));
        });
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();

        return services;
    }
}