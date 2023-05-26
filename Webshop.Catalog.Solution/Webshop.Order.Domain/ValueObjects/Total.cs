using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain;

[ValueObject<decimal>]
[Instance("Zero", 0)]
public partial struct Total 
{
    public static Validation Validate(decimal amount)
        => amount > 0 
            ? Validation.Ok
            : Validation.Invalid(Errors.General.ValueTooSmall(nameof(amount), 0, "That'd be giving money away.").Message.Value);
}