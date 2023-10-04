using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankDeposits.Mvc.Controllers;

public class AccountController : BaseController<Account, AccountService>
{
    public AccountController(AccountService service) : base(service) {}

    public override IActionResult Create()
    {
        ViewBag.Depositors = new SelectList(Context.Depositors, nameof(Depositor.Id), nameof(Depositor.PassportSeries));

        return View();
    }
}