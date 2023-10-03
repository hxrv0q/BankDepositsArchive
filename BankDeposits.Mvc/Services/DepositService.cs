using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public class DepositService : BaseService<Deposit>
{
    public DepositService(BankDepositsContext context) : base(context)
    {
    }

    protected override DbSet<Deposit> Entities => Context.Deposits;
}