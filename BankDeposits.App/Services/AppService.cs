using BankDeposits.App.Database;
using BankDeposits.App.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BankDeposits.App.Services;

/// <summary>
///  Provides service to perform operations related to the application.
/// </summary>
public class AppService
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="AppService"/> class.
    /// </summary>
    /// <param name="dbContext">The application's database context.</param>
    public AppService(AppDbContext dbContext) => _dbContext = dbContext;

    /// <summary>
    /// Gets all depositors with multiple visits.
    /// </summary>
    /// <param name="minVisits">The minimum number of visits.</param>
    /// <returns>A list of <see cref="DepositorVisits"/> objects.</returns>
    public async Task<List<DepositorVisits>> GetDepositorsWithMultipleVisits(int minVisits)
    {
        var depositors = await GetDepositorsWithIncludedRelations();
        return FilterAndProjectDepositors(depositors, minVisits);
    }

    private async Task<List<Depositor>> GetDepositorsWithIncludedRelations() => await _dbContext.Depositors
        .Find(_ => true)
        .ToListAsync();

    private static List<DepositorVisits> FilterAndProjectDepositors(IEnumerable<Depositor> depositors, int minVisits) =>
        depositors.Select(depositor => new DepositorVisits
                { Depositor = depositor, Visits = depositor.Accounts.Sum(account => account.Deposits.Count) })
            .Where(dv => dv.Visits >= minVisits).ToList();


    /// <summary>
    /// Represents a depositor with the number of visits.
    /// </summary>
    public class DepositorVisits
    {
        public Depositor Depositor { get; init; } = null!;
        public int Visits { get; init; }
    }
}