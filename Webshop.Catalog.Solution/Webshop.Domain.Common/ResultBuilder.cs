using System.Runtime.InteropServices.JavaScript;
using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Domain.Common;

public class ResultBuilder<T>
{
    public Func<T> Mapper { get; set; }

    public Func<Exception, Error> ErrorHandler { get; set; }
        = (e) => Errors.General.UnspecifiedError(e.Message);

    public ResultBuilder<T> Try(Func<T> mapper)
    {
        Mapper = mapper;
        return this;
    }

    public ResultBuilder<T> Catch(Func<Exception, Error> errorHandler)
    {
        ErrorHandler = errorHandler;
        return this;
    }

    public Result<T> Build()
    {
        try
        {
            return Result.Ok(Mapper());
        }
        catch (ValueObjectValidationException e)
        {
            return Result.Fail<T>(ErrorHandler(e));
        }
    }
}