using Webshop.Application.Contracts;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Abstractions;

public class GetVoucherQuery : IQuery<Discount>
{
    public string? Code { get; set; }
}