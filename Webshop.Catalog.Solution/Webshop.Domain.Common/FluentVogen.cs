using System.Runtime.InteropServices.JavaScript;
using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Domain.Common;

public class FluentVogenBuilder<T>
{
    public Func<T> Mapper { get; set; }

    public Func<ValueObjectValidationException, Error> ErrorHandler { get; set; }
        = (e) => Errors.General.UnspecifiedError(e.Message);

    public FluentVogenBuilder<T> UseMapper(Func<T> mapper)
    {
        Mapper = mapper;
        return this;
    }

    public FluentVogenBuilder<T> UseError(Func<ValueObjectValidationException, Error> errorHandler)
    {
        ErrorHandler = errorHandler;
        return this;
    }

    public Result<T> Run()
    {
        return VogenHelper.ToResult(Mapper, ErrorHandler);
    }
}

public static class FluentVogen
{
    public static FluentVogenBuilder<T> UseMapper<T>(Func<T> mapper)
        => new FluentVogenBuilder<T>().UseMapper(mapper);
}
