using Vogen;
using System.Text.Json;
using Webshop.Domain.Common;
using Webshop.Order.Domain;
using Webshop.Order.Domain.Dto;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Application;

public class CatalogClientOptions
{
    public required Uri Uri { get; set; }
}

public class CatalogClient
{
    private readonly HttpClient client;
    private readonly CatalogClientOptions options;

    public CatalogClient(HttpClient client, CatalogClientOptions options)
    {
        this.client = client;
        this.options = options;
    }

    public async Task<Result<Product>> GetAsync(int id)
    {
        var request = await client.GetAsync($"{options.Uri}?id={id}");
        var result = await request.Content.ReadAsStreamAsync();
        var dto = JsonSerializer.Deserialize<ProductDto>(result);

        if (dto is null)
        {
            return Result.Fail<Product>(Errors.General.NotFound(id));
        }

        try
        {
            var model = dto.ToModel();

            return Result.Ok(model);
        }
        catch (ValueObjectValidationException e)
        {
            return Result.Fail<Product>(Errors.General.UnexpectedValue(e.Message));
        }
    }

    public async Task<Result<IEnumerable<Product>>> GetAllAsync(IEnumerable<int> ids)
    {
        var products = new List<Product>();

        foreach(var id in ids)
        {
            // TODO: This is incredibly ineffecient, but good enough for this case.
            products.Add(await GetAsync(id));
        }

        return Result.Ok<IEnumerable<Product>>(products);
    }
}