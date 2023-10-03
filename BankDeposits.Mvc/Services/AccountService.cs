using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public class AccountService : BaseService<Account>
{
    public AccountService(BankDepositsContext context) : base(context)
    {
    }

    protected override DbSet<Account> Entities => Context.Accounts;
}