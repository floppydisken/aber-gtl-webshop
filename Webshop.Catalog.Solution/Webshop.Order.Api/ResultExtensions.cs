using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Webshop.Domain.Common;

namespace Webshop.Order.Api;

public static class ResultExtensions
{
    public static IActionResult ToResponse(this Result result)
    {
        if (result.Failure)
        {
            return new BadRequestObjectResult(result.Error);
        }

        return new OkResult();
    }
    
    public static IActionResult ToResponse<T>(this Result<T> result)
    {
        if (result.Failure)
        {
            return new BadRequestObjectResult(result.Error);
        }

        return new OkObjectResult(result.Value);
    }
}