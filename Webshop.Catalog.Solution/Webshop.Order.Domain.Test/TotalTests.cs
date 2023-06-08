using Vogen;

namespace Webshop.Order.Domain.Test;

public class TotalTests
{ 
    [Fact]
    public void FailsWhenLessThan0()
    {
        Assert.Throws<ValueObjectValidationException>(() => Total.From(-0.0001m));
    }

    [Fact]
    public void CreatesTotalWhenLargerThanOrEqual0()
    {
        var result = Total.From(0.0001m);
        Assert.Equal(0.0001m, result.Value);

        result = Total.From(Decimal.MaxValue);
        Assert.Equal(Decimal.MaxValue, result.Value);
    }
}