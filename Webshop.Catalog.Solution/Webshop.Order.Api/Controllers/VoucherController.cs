using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Api;
using Webshop.Application.Contracts;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain.Dto;

namespace Webshop.Order.Api.Controllers;

[Route("api/vouchers")]
public class VoucherController : WebshopController
{
    private readonly IDispatcher dispatcher;

    public VoucherController(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVoucherAsync([FromBody] CreateVoucherCommand request)
    {
        var result = await dispatcher.Dispatch(request);

        return result.ToResponse();
    }

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(VoucherDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetVoucherAsync(string code)
    {
        var result = await dispatcher.Dispatch(new GetVoucherQuery {Code = code});

        return result.ToResponse();
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> DeleteVoucherAsync(string code)
    {
        var result = await dispatcher.Dispatch(new DeleteVoucherCommand {Code = code});
        
        return result.ToResponse();
    }

    [HttpPost("storewide")]
    public async Task<IActionResult> SetStorewideDiscountAsync([FromBody] SetStoreWideDiscountCommand request)
    {
        var result = await dispatcher.Dispatch(request);

        return result.ToResponse();
    }

    [HttpGet("storewide")]
    [ProducesResponseType(typeof(VoucherDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetStorewideDiscountAsync()
    {
        var result = await dispatcher.Dispatch(new GetStoreWideDiscountQuery());
        
        return result.ToResponse();
    }
}