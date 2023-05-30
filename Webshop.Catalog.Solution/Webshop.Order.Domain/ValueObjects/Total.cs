using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain;

[ValueObject<decimal>]
[Instance("Zero", 0)]
[Instance("Min", 0)]
public partial struct Total 
{
    private static readonly decimal MinValue = 0;

    public static Validation Validate(decimal amount)
        => amount > Total.MinValue
            ? Validation.Ok
            : Validation.Invalid(Errors.General.ValueTooSmall(nameof(amount), Total.MinValue, "That'd be giving money away.").Message.Value);

    public static Total FromOrBoundary(decimal amount)
        => amount > Total.MinValue ? Total.From(amount) : Total.Min;
}