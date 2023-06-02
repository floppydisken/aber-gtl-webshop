using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Webshop.Order.Application;
using Serilog;

namespace Webshop.Order.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        string sequrl =
            Configuration.GetValue<string>("Settings:SeqLogAddress");
        Log.Logger =
            new LoggerConfiguration()
                .MinimumLevel
                .Information()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Service", "Payment.Api") //enrich with the tag "service" and the name of this service
                .WriteTo.Seq(sequrl)
                .CreateLogger();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddOrderServices();
        services
            .AddSwaggerGen(c =>
            {
                c
                    .SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Webshop.Order.Api",
                        Version = "v1"
                    });
            });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        ILoggerFactory loggerFactory
    )
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app
            .UseSwaggerUI(c =>
                c
                    .SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Order Gateway v1"));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        loggerFactory.AddSerilog();
    }
}