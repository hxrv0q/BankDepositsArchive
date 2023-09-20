using BankDeposits.App.Database;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.App.Services;

public class AppService
{
    private readonly AppDbContext _dbContext;

    public AppService(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<object>> GetDepositorsWithMultipleVisits(int minVisits) =>
        await _dbContext.Depositors.Include(d => d.Accounts)
            .ThenInclude(a => a.Deposits)
            .ToListAsync()
            .ContinueWith(t =>
                t.Result
                    .SelectMany(d => d.Accounts)
                    .SelectMany(a => a.Deposits)
                    .GroupBy(d => new
                    {
                        d.Account.Depositor.Id, d.Account.Depositor.LastName, d.Account.Depositor.FirstName,
                        d.Account.Depositor.Patronymic
                    })
                    .Select(g => new
                    {
                        g.Key.Id,
                        g.Key.LastName,
                        g.Key.FirstName,
                        g.Key.Patronymic,
                        VisitCount = g.Count()
                    })
                    .Where(g => g.VisitCount >= minVisits)
                    .OrderBy(g => -g.VisitCount)
                    .ToList<object>()
            );
}