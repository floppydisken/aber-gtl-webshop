using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<int>]
[Instance("Zero", 0)]
public partial struct Quantity 
{
    public static Validation Validate(int value) =>
        value >= 0 ? Validation.Ok : Validation.Invalid(Errors.General.ValueTooSmall(nameof(value), 0).Message.Value);
}