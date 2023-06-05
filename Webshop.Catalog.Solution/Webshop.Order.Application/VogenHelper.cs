using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Application;

public static class VogenHelper
{
    public static Result<T> ToResult<T>(Func<T> map, Func<ValueObjectValidationException, Error> errorHandler)
    {
        try
        {
            return Result.Ok(map());
        }
        catch (ValueObjectValidationException e)
        {
            return Result.Fail<T>(errorHandler(e));
        }
    }
}
