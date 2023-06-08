using Vogen;

namespace Webshop.Order.Domain.Test;

public class QuantityTests
{ 
    [Fact]
    public void FailsWhenLessThan0()
    {
        Assert.Throws<ValueObjectValidationException>(() => Quantity.From(-1));
    }

    [Fact]
    public void CreatesQuantityWhenPositiveInteger()
    {
        var result = Quantity.From(0);
        Assert.Equal(0, result.Value);

        result = Quantity.From(Int32.MaxValue);
        Assert.Equal(Int32.MaxValue, result.Value);
    }
}