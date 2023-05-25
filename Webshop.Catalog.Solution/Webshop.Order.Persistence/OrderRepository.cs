using Webshop.Order.Persistence.Abstractions;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Webshop.Order.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly MongoClient client;
    private readonly IMongoCollection<Domain.AggregateRoots.Order> collection;

    public OrderRepository(MongoClient client)
    {
        this.client = client;
        this.collection = client.GetDatabase("orders")
            .GetCollection<Domain.AggregateRoots.Order>("orders");
    }

    public async Task CreateAsync(Domain.AggregateRoots.Order entity)
        => await collection.InsertOneAsync(entity);

    public async Task DeleteAsync(int id)
        => await collection.FindOneAndDeleteAsync(o => o.Id == id);

    public async Task<IEnumerable<Domain.AggregateRoots.Order>> GetAll()
        => await this.collection.Aggregate().ToListAsync();

    public async Task<Domain.AggregateRoots.Order> GetById(int id)
        => await this.collection
            .Find<Domain.AggregateRoots.Order>(o => o.Id == id)
            .FirstOrDefaultAsync();

    public async Task UpdateAsync(Domain.AggregateRoots.Order entity)
        => await this.collection.FindOneAndReplaceAsync(o => o.Id == entity.Id, entity);
}
