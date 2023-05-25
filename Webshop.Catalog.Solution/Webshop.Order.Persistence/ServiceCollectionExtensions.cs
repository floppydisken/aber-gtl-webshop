using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Webshop.Order.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderMongoPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoClient>(opts => new MongoClient(new MongoUrl("")));
        services.AddMongo
        services.AddScoped<IOrderReposiorty, OrderRepository>();
    }
}