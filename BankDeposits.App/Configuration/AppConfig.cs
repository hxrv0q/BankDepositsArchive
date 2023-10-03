using Microsoft.Extensions.Configuration;

namespace BankDeposits.App.Configuration;

public static class AppConfig
{
    private const string DefaultEnvironment = "Development";
    private const string EnvironmentVariableName = "NETCORE_ENVIRONMENT";

    /// <summary>
    /// Loads the configuration from appsettings.json and environment variables.
    /// </summary>
    /// <returns>An <see cref="IConfigurationRoot"/> with the loaded configuration.</returns>
    public static IConfigurationRoot LoadConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable(EnvironmentVariableName) ?? DefaultEnvironment;
        return BuildConfiguration(environment);
    }

    private static IConfigurationRoot BuildConfiguration(string environment) => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.{environment}.json", optional: false)
        .AddEnvironmentVariables()
        .Build();
}
