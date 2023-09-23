using System.Text.Json;
using System.Text.Json.Serialization;
using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using BankDeposits.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            var data = await appService.GetDepositorsWithMultipleVisits(2);

            context.Response.ContentType = "application/json";

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            await JsonSerializer.SerializeAsync(context.Response.Body, data, options);
        });
    })
    .Build();

host.Run();