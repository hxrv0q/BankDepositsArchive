using System.Text.Json;
using BankDeposits.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BankDeposits.App.Http;

public class WebServer
{
    private readonly AppService _appService;

    public WebServer(AppService appService) => _appService = appService;

    public IWebHost BuildWebHost()
    {
        return new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(services => { services.AddSingleton(_appService); })
            .Configure(Config)
            .Build();
    }

    private void Config(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var depositors = _appService.GetDepositorsWithTwoVisits();
            await RespondWithJson(context, depositors);
        });
    }

    private static async Task RespondWithJson(HttpContext context, object data)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(data));
    }
}