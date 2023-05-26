namespace Webshop.Order.Domain.Test;

public class NonEmptyStringTests
{
    [Fact]
    public void FailsOnEmpty()
    {
        Assert.Throws<ValueObjectValidationException>(() => NonEmptyString.From(""));
        Assert.Throws<ValueObjectValidationException>(() => NonEmptyString.From(null));
    }

    [Fact]
    public void DoesNotFailWhenAnything()
    {
        Assert.Equal("H", NonEmptyString.From("H").Value);
    }
}