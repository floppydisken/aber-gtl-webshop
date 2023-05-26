using Vogen;

namespace Webshop.Order.Domain.Test;

public class DiscountTests
{ 
    [Fact]
    public void FailsWhenLessThan0()
    {
        Assert.Throws<ValueObjectValidationException>(() => Discount.From(-0.0001m));
    }

    [Fact]
    public void FailsWhenLargetThan15()
    {
        Assert.Throws<ValueObjectValidationException>(() => Discount.From(15.0001m));
    }

    [Fact]
    public void CreatesDiscountWithin0And15Pct()
    {
        var result = Discount.From(0.0001m);
        Assert.Equal(0.0001m, result.Value);

        result = Discount.From(14.9999m);
        Assert.Equal(14.9999m, result.Value);
    }
}