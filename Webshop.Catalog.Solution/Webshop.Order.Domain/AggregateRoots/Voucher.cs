using Webshop.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.AggregateRoots;

public class Voucher : AggregateRoot
{
    public VoucherCode Code { get; set; }
    public Discount Discount { get; set; }
}