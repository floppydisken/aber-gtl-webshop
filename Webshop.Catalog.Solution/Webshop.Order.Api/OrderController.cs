using Microsoft.AspNetCore.Mvc;
using Webshop.Api;

namespace Webshop.Order.Api;

[Route("api/orders")]
public class OrderController : WebshopController
{
    [HttpPost]
    public Task<IActionResult> PostAsync()
    {
        throw new NotImplementedException();
    }
}
