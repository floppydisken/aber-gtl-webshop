using Vogen;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<string>]
public partial struct NonEmptyString
{
    public static Validation Validate(string value)
        => string.IsNullOrWhiteSpace(value) 
            ? Validation.Invalid($"'{nameof(value)}' cannot be empty.") 
            : Validation.Ok;
}