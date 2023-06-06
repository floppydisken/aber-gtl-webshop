using MongoDB.Driver;
using Webshop.Order.Domain;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Persistence;

public class VoucherRepository : IVoucherRepository
{
    private readonly IMongoCollection<Domain.Dto.VoucherDto> collection;

    public VoucherRepository(IMongoClient client)
    {
        collection = client
            .GetDatabase("orders")
            .GetCollection<Domain.Dto.VoucherDto>("vouchers");
    }

    public async Task CreateAsync(Voucher entity)
    {
        var existing = await collection
            .Find(v => v.Code == entity.Code)
            .FirstOrDefaultAsync();

        if (existing is not null)
        {
            await UpdateAsync(entity);
            return;
        }
        
        await collection.InsertOneAsync(entity.ToDto());
    }

    public async Task DeleteAsync(int id)
        => await collection.FindOneAndDeleteAsync(o => o.Id == id);

    public async Task<IEnumerable<Voucher>> GetAll()
        => (await collection
                .Aggregate()
                .ToListAsync())
            .Select(o => o.ToModel().Unwrap());

    public async Task<Voucher> GetByIdAsync(int id)
        => (await collection
                .Find(o => o.Id == id) 
                .FirstOrDefaultAsync())
            .ToModel()
            .Unwrap();

    public async Task UpdateAsync(Voucher entity)
        => await collection
            .FindOneAndReplaceAsync(o => o.Id == entity.Id, entity.ToDto());

    public async Task<Voucher?> GetByCodeAsync(VoucherCode code)
        => (await collection
                .Find(v => v.Code == code.Value)
                .FirstOrDefaultAsync())?
            .ToModel()
            .UnwrapOrDefault();

    public async Task DeleteByCodeAsync(VoucherCode code)
        => await collection.DeleteOneAsync(v => v.Code == code.Value);
}
