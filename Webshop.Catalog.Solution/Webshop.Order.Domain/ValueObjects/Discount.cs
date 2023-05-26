using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<decimal>]
[Instance("Zero", 0)]
public partial struct Discount
{
    public static Validation Validate(decimal value) =>
        value < 0 
            ? Validation.Invalid(Errors.General.ValueTooSmall(nameof(value), 0).Message.Value) 
            : value > 15 
            ? Validation.Invalid(Errors.General.ValueTooLarge(nameof(value), 15).Message.Value) 
            : Validation.Ok;
}