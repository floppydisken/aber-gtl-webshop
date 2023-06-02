using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.Api;
using Webshop.Order.Application.Abstractions;

namespace Webshop.Order.Api;

[Route("api/orders")]
public class OrderController : WebshopController
{
    private readonly IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public Task<IActionResult> PostAsync([FromBody] BuyCommand request)
    {
        mediator.Send(request);

        return Task.FromResult(Ok() as IActionResult);
    }

    [HttpGet("{id}")]
    public Task<IActionResult> GetAsync(int id)
    {
        // return mediator.Send(new GetOrderQuery { OrderId = id });
        throw new NotImplementedException();
    }
}
