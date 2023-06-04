using Webshop.Order.Persistence.Abstractions;
using MongoDB.Driver;
using MongoDB.Bson;
using Webshop.Order.Domain;

namespace Webshop.Order.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoClient client;
    private readonly IMongoCollection<Domain.Dto.OrderDto> collection;

    public OrderRepository(IMongoClient client)
    {
        this.client = client;
        this.collection = client
            .GetDatabase("orders")
            .GetCollection<Domain.Dto.OrderDto>("orders");
    }

    public async Task CreateAsync(Domain.AggregateRoots.Order entity)
        => await collection.InsertOneAsync(entity.ToDto());

    public async Task DeleteAsync(int id)
        => await collection.FindOneAndDeleteAsync(o => o.Id == id);

    public async Task<IEnumerable<Domain.AggregateRoots.Order>> GetAll()
        => (await this.collection.Aggregate().ToListAsync()).Select(o => o.ToModel().Unwrap());

    public async Task<Domain.AggregateRoots.Order> GetByIdAsync(int id)
        => (await this.collection
            .Find(o => o.Id == id)
            .FirstOrDefaultAsync())
            .ToModel()
            .Unwrap();

    public async Task UpdateAsync(Domain.AggregateRoots.Order entity)
        => await this.collection.FindOneAndReplaceAsync(o => o.Id == entity.Id, entity.ToDto());
}
