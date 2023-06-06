using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Api;
using Webshop.Application.Contracts;
using Webshop.Order.Application.Abstractions;

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
    public async Task<IActionResult> GetStorewideDiscountAsync()
    {
        var result = await dispatcher.Dispatch(new GetStoreWideDiscountQuery());
        
        return result.ToResponse();
    }
}