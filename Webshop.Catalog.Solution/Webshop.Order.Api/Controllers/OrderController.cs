using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Api;
using Webshop.Application.Contracts;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain;
using Webshop.Order.Domain.Dto;

namespace Webshop.Order.Api.Controllers;

[Route("api/orders")]
public class OrderController : WebshopController
{
    private readonly IDispatcher dispatcher;

    public OrderController(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> BuyAsync([FromBody] BuyCommand request)
    {
        var result = await dispatcher.Dispatch(request);

        return Ok(result);
    }

    [HttpPost("{id}/pay")]
    public async Task<IActionResult> PayAsync([FromBody] PayForOrderCommand request)
    {
        var result = await dispatcher.Dispatch(request);
        
        return result.ToResponse();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = (await dispatcher.Dispatch(new GetOrderQuery() { OrderId = id }));

        return Ok(result.Value.ToDto().Unwrap());
    }
}
