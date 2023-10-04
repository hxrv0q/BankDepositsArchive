using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class DepositService : AbstractService<Deposit>
{
    public DepositService(BankDepositsContext context) : base(context) { }
}