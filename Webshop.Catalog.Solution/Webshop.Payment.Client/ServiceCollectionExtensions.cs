using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Webshop.Payment.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentClient(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<IPaymentClient, PaymentClient>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var uri = new Uri(config["Payments:Uri"]);
            var client = provider.GetRequiredService<HttpClient>();

            return new PaymentClient(client, new() { PaymentUri = uri });
        });

        return services;
    }
}