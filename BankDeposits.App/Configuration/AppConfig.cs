using Microsoft.Extensions.Configuration;

namespace BankDeposits.App.Configuration;

public static class AppConfig
{
    public static IConfigurationRoot LoadConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT") ?? "Development";

        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environment}.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
    }
}