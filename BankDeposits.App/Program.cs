using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using BankDeposits.App.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = AppConfig.LoadConfiguration();
var connectionString = configuration.GetConnectionString("BankDepositsDatabase");

try
{
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlServer(connectionString);

    await using var dbContext = new AppDbContext(optionsBuilder.Options);
    var appService = new AppService(dbContext); 
     
    var depositors = await appService.GetDepositorsWithMultipleVisits(2);

    foreach (var depositor in depositors)
    {
        Console.WriteLine(depositor);
    }
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
}