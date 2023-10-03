using BankDeposits.App.Database;
using BankDeposits.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.App.Services;

/// <summary>
///  Provides service to perform operations related to the application.
/// </summary>
public class AppService
{
    private readonly AppDbContext _dbContext;

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
        .Include(d => d.Accounts)
        .ThenInclude(a => a.Deposits)
        .ToListAsync();

    private static List<DepositorVisits> FilterAndProjectDepositors(IEnumerable<Depositor> depositors, int minVisits) =>
        depositors
            .SelectMany(depositor => depositor.Accounts)
            .SelectMany(account => account.Deposits, (account, deposit) => new { account, deposit })
            .GroupBy(ad => ad.account.Depositor)
            .Where(grouping => grouping.Count() >= minVisits)
            .Select(grouping => new DepositorVisits { Depositor = grouping.Key, Visits = grouping.Count() })
            .OrderByDescending(dv => dv.Visits)
            .ToList();


    /// <summary>
    /// Represents a depositor with the number of visits.
    /// </summary>
    public class DepositorVisits
    {
        public Depositor Depositor { get; init; } = null!;
        public int Visits { get; init; }
    }
}
