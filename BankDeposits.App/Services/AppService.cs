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
    public async Task<List<DepositorVisits>> GetDepositorsWithMultipleVisits(int minVisits) => await _dbContext
        .Depositors
        .SelectMany(d => d.Accounts)
        .SelectMany(a => a.Deposits)
        .GroupBy(d => d.Account.Depositor)
        .Select(grouping => new DepositorVisits
        {
            Depositor = grouping.Key,
            Visits = grouping.Count()
        })
        .Where(dv => dv.Visits >= minVisits)
        .OrderByDescending(dv => dv.Visits)
        .ToListAsync();


    /// <summary>
    /// Represents a depositor with the number of visits.
    /// </summary>
    public class DepositorVisits
    {
        public Depositor Depositor { get; init; } = null!;
        public int Visits { get; init; }
    }
}