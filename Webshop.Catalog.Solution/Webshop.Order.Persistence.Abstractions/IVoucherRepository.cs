using Webshop.Application.Contracts.Persistence;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Persistence.Abstractions;

public interface IVoucherRepository : IRepository<Voucher>
{
    public Task<Voucher?> GetByCodeAsync(string code);
    public Task DeleteByCodeAsync(string code);
}