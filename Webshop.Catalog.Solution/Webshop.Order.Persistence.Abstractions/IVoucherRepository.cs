using Webshop.Application.Contracts.Persistence;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Persistence.Abstractions;

public interface IVoucherRepository : IRepository<Voucher>
{
    public Task<Voucher?> GetByCodeAsync(VoucherCode code);
    public Task DeleteByCodeAsync(VoucherCode code);
}