using BankDeposits.App.Database;

namespace BankDeposits.App.Services;

public class AppService
{
   private readonly AppDbContext _dbContext;
   
   public AppService(AppDbContext dbContext) => _dbContext = dbContext;

   public List<object> GetDepositorsWithTwoVisits()
   {
      return _dbContext.Depositors
         .SelectMany(depositor => depositor.Accounts)
         .SelectMany(account => account.Deposits)
         .GroupBy(deposit => new
         {
            deposit.Account.Depositor.Id,
            deposit.Account.Depositor.LastName,
            deposit.Account.Depositor.FirstName,
            deposit.Account.Depositor.Patronymic,
         }).Select(group => new
         {
            group.Key.Id,
            group.Key.LastName,
            group.Key.FirstName,
            group.Key.Patronymic,
            visitCount = group.Count()
         }).Where(group => group.visitCount > 1)
         .OrderBy(group => group.visitCount)
         .ToList<object>();
   }
}