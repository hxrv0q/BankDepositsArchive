using BankDeposits.App.Configuration;
using BankDeposits.App.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BankDeposits.App.Database;

/// <summary>
/// Represents a database context for the application.
/// </summary>
public class AppDbContext
{
    public IMongoCollection<Depositor> Depositors { get; }

    public AppDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("BankDepositsDatabase"));
        var database = client.GetDatabase("BankDeposits");
        Depositors = database.GetCollection<Depositor>("Bank");
    }
}