using Vogen;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<int>]
public partial struct Quantity 
{
    public static Validation Validate(int value) =>
        value >= 0 ? Validation.Ok : Validation.Invalid("Must be greater than -1.");
}