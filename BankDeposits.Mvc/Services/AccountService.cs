using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class AccountService : AbstractService<Account>
{
    public AccountService(BankDepositsContext context) : base(context) { }
}