using Webshop.Domain.Common;
using System.Text.Json;
using Webshop.Order.Domain.Dto;
using Webshop.Order.Domain;
using Vogen;

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

        try 
        {
            var model = dto?.ToModel();

            if (model is null)
            {
                return Result.Fail(Errors.General.NotFound(id));
            }

            return Result.Ok(model);
        }
        catch(ValueObjectValidationException e)
        {
            return Result.Fail(Errors.General.UnexpectedValue(e.Message));
        }
    }

    public Task<Result<IEnumerable<Product>>> GetAllAsync()
    {

    }
}