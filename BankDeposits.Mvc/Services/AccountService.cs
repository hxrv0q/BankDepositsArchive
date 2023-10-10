using BankDeposits.Mvc.Data;
using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class AccountService : AbstractService<Account>
{
    public AccountService(AppDbContext context) : base(context) { }
}