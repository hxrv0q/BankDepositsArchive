using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;

namespace BankDeposits.Mvc.Controllers;

public class AccountController : BaseController<Account, AccountService>
{
    public AccountController(AccountService service) : base(service)
    {
    }
}