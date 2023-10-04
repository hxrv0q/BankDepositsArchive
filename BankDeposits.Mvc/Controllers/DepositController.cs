using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankDeposits.Mvc.Controllers;

public class DepositController : BaseController<Deposit, DepositService>
{
    public DepositController(DepositService service) : base(service) {}

    public override IActionResult Create()
    {
        ViewBag.Accounts = new SelectList(Context.Accounts, nameof(Account.Id), nameof(Account.Number));

        return View();
    }
}