using Webshop.Domain.Common;

namespace Webshop.Order.Domain.Test;

class TestEntity : Entity {}

public class NonEmptyListTests
{
    [Fact]
    public void ReturnsErrorResultOnEmpty()
    {
        var result = NonEmptyList.From(new Entity[] { });
        Assert.True(result.Failure);
    }

    [Fact]
    public void ReturnsListOnNotEmpty()
    {
        var result = NonEmptyList.From(new [] { new TestEntity() });
        Assert.NotNull(result.Unwrap());
    }
}