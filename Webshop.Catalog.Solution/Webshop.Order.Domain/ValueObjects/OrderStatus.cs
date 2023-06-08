using Vogen;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<string>]
[Instance("Created", "created")]
[Instance("Pending", "pending")]
[Instance("Completed", "completed")]
public partial struct OrderStatus
{
}