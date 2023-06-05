using Vogen;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<string>]
[Instance("Invalid", "INVALID")]
[Instance("StoreWide", "STORE_WIDE")]
public partial struct VoucherCode
{
}