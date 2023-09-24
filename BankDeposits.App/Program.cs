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

var host = new WebHostBuilder()
    .UseKestrel()
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BankDepositsDatabase")));

        services.AddScoped<AppService>();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    })
    .Configure(app =>
    {
        app.UseRouting();

        app.Use(async (context, next) =>
        {
            context.Response.ContentType = "application/json";
            await next.Invoke();
        });

        app.UseEndpoints(endpoints => {
            endpoints.MapGet("/", async context => {
                var appService = context.RequestServices.GetRequiredService<AppService>();
                var data = await appService.GetDepositorsWithMultipleVisits(2);
                await JsonSerializer.SerializeAsync(context.Response.Body, data);
            });
        });    
    })
    .Build();

host.Run();
