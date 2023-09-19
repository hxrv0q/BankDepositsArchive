using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = AppConfig.LoadConfiguration();
var connectionString = configuration.GetConnectionString("BankDepositsDatabase")
                       ?? throw new Exception("Connection string is not found");

try
{
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlServer(connectionString);

    using var dbContext = new AppDbContext(optionsBuilder.Options);

    var query = dbContext.Depositors
        .Include(depositor => depositor.Accounts)
        .ThenInclude(account => account.Deposits)
        .AsEnumerable()
        .SelectMany(d => d.Accounts)
        .SelectMany(a => a.Deposits)
        .GroupBy(d => new
        {
            d.Account.Depositor.Id,
            d.Account.Depositor.LastName,
            d.Account.Depositor.FirstName,
            d.Account.Depositor.Patronymic
        })
        .Select(g => new
        {
            g.Key.Id,
            g.Key.LastName,
            g.Key.FirstName,
            g.Key.Patronymic,
            visitCount = g.Count()
        })
        .Where(g => g.visitCount > 1)
        .OrderBy(g => g.visitCount)
        .ToList();

    foreach (var depositor in query)
    {
        Console.WriteLine($"${depositor.Id} {depositor.LastName} {depositor.FirstName} {depositor.Patronymic} {depositor.visitCount}");
    }
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
}