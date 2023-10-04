using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class AccountService : BaseService<Account>
{
    public AccountService(BankDepositsContext context) : base(context) { }
}