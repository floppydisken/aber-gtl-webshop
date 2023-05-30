using System.Collections;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

/// <summary>
/// List that enforces a size of above 1 on initialization, is also comparable with other lists of entities.
/// </summary>
public class NonEmptyEntityList<T> : ValueObject, IEnumerable<T>
    where T : Entity
{
    private IList<T> list; 

    private NonEmptyEntityList(IEnumerable<T> initial) 
    {
        list = initial.ToList();
    }

    public static Result<NonEmptyEntityList<T>> From(IEnumerable<T> initial)
    {
        if (!initial.Any()) 
        { 
            return Result.Fail<NonEmptyEntityList<T>>(Errors.General.ValueIsEmpty("The list cannot be empty.")); 
        }

        var list = new List<T>(initial.Count());

        return new NonEmptyEntityList<T>(initial);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return this.list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.list.GetEnumerator();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return this.list.AsReadOnly().OrderBy(e => e.Id);
    }

}

/// <summary>
/// More ergonomic functions for creation of the NonEmptyList
/// </summary>
public static class NonEmptyEntityList
{
    public static Result<NonEmptyEntityList<T>> From<T>(IEnumerable<T> initial)
        where T : Entity
        => NonEmptyEntityList<T>.From(initial);
}