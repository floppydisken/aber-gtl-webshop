using System.Text.Json;
using Vogen;
using Webshop.Customer.Application.Features.Dto;
using Webshop.Domain.Common;

namespace Webshop.Order.Application;

public static class CustomerDomainMapperExtensions
{
    public static Result<Customer.Domain.AggregateRoots.Customer> ToModel(this CustomerDto customer)
    {
        return Result.Ok<Customer.Domain.AggregateRoots.Customer>(new()
        {
            Address = customer.Address,
            Address2 = customer.Address2,
            City = customer.City,
            Country = customer.Country,
            PostalCode = customer.PostalCode,
            Id = customer.Id,
            Email = customer.Email,
            Region = customer.Region,
        });
    }
    
    public static Result<CustomerDto> ToDto(this Customer.Domain.AggregateRoots.Customer customer)
    {
        return Result.Ok<CustomerDto>(new()
        {
            Address = customer.Address,
            Address2 = customer.Address2,
            City = customer.City,
            Country = customer.Country,
            PostalCode = customer.PostalCode,
            Id = customer.Id,
            Email = customer.Email,
            Region = customer.Region,
        });
    }
}

public class CustomerClientOptions
{
    public Uri Uri { get; set; } = new ("http://0.0.0.0:8085");
}

public class CustomerClient
{
    private readonly HttpClient client;
    private readonly CustomerClientOptions options;

    public CustomerClient(HttpClient client, CustomerClientOptions options)
    {
        this.client = client;
        this.options = options;
    }

    public async Task<Result<Customer.Domain.AggregateRoots.Customer>> GetAsync(int id)
    {
        var request = await client.GetAsync($"{options.Uri}/api/customers/{id}");
        var result = await request.Content.ReadAsStreamAsync();
        var dto = JsonSerializer.Deserialize<CustomerDto>(result);

        if (dto is null)
        {
            return Result.Fail<Customer.Domain.AggregateRoots.Customer>(Errors.General.NotFound(id));
        }

        try
        {
            var model = dto.ToModel();
            return Result.Ok(model);
        }
        catch (ValueObjectValidationException e)
        {
            return Result.Fail<Customer.Domain.AggregateRoots.Customer>(Errors.General.UnexpectedValue(e.Message));
        }
    }
}