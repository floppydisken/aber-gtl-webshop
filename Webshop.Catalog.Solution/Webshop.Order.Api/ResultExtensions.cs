using Microsoft.AspNetCore.Mvc;
using Webshop.Domain.Common;

namespace Webshop.Order.Api;

public static class ResultExtensions
{
    private static IActionResult ToErrorResponse(this Error error)
    {
        if (error.Code == "entity.not.found")
        {
            return new NotFoundObjectResult(error);
        }
        
        return new BadRequestObjectResult(error);
    }
    
    public static IActionResult ToResponse(this Result result)
    {
        if (result.Failure)
        {
            return result.Error.ToErrorResponse();
        }

        return new OkResult();
    }
    
    public static IActionResult ToResponse<T>(this Result<T> result)
    {
        if (result.Failure)
        {
            return result.Error.ToErrorResponse();
        }

        return new OkObjectResult(result.Value);
    }
}