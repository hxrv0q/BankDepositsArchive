using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;

namespace BankDeposits.Mvc.Controllers;

public class DepositController : BaseController<Deposit, DepositService>
{
    public DepositController(DepositService service) : base(service)
    {
    }
}