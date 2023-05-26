using System.Collections;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

/// <summary>
/// List that enforces a size of above 1 on initialization, is also comparable with other lists of entities.
/// </summary>
public class NonEmptyList<T> : ValueObject
    where T : Entity
{
    private IList<T> list; 

    private NonEmptyList(IEnumerable<T> initial) 
    {
        list = initial.ToList();
    }

    public static Result<NonEmptyList<T>> From(IEnumerable<T> initial)
    {
        if (!initial.Any()) 
        { 
            return Result.Fail<NonEmptyList<T>>(Errors.General.ValueIsEmpty("The list cannot be empty.")); 
        }

        var list = new List<T>(initial.Count());

        return new NonEmptyList<T>(initial);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return this.list.AsReadOnly().OrderBy(e => e.Id);
    }
}

/// <summary>
/// More ergonomic functions for creation of the NonEmptyList
/// </summary>
public static class NonEmptyList
{
    public static Result<NonEmptyList<T>> From<T>(IEnumerable<T> initial)
        where T : Entity
        => NonEmptyList<T>.From(initial);
}