using BankDeposits.Mvc.Data;
using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class DepositService : AbstractService<Deposit>
{
    public DepositService(AppDbContext context) : base(context) { }
}