using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using BankDeposits.App.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

try
{
    var configuration = AppConfig.LoadConfiguration();
    var connectionString = configuration.GetConnectionString("BankDepositsDatabase");

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlServer(connectionString);

    await using var dbContext = new AppDbContext(optionsBuilder.Options);
    var appService = new AppService(dbContext);

    var depositorVisits = await appService.GetDepositorsWithMultipleVisits(2);
    Console.WriteLine("| Id | Last Name | First Name | Visits |");
    foreach (var dv in depositorVisits)
    {
        Console.WriteLine($"| {dv.Depositor.Id} | {dv.Depositor.LastName} | {dv.Depositor.FirstName} | {dv.Visits} |");
    }
}
catch (Exception exception)
{
    Console.Error.WriteLine(exception);
}
