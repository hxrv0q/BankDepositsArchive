using System.Text.Json;
using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using BankDeposits.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = AppConfig.LoadConfiguration();
var connectionString = configuration.GetConnectionString("BankDepositsDatabase");

var host = new WebHostBuilder()
    .UseKestrel()
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<AppService>();
    })
    .Configure(app =>
    {
        app.Run(async context =>
        {
            var appService = context.RequestServices.GetRequiredService<AppService>();
            var data = appService.GetDepositorsWithMultipleVisits(2);
            
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(data));
        });
    })
    .Build();

host.Run();