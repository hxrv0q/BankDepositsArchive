using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class DepositService : BaseService<Deposit>
{
    public DepositService(BankDepositsContext context) : base(context) { }
}