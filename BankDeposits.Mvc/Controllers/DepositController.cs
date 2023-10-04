using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankDeposits.Mvc.Controllers;

public class DepositController : BaseController<Deposit, DepositService>
{
    private readonly AccountService _accountService;

    public DepositController(DepositService depositService, AccountService accountService) : base(depositService)
    {
        _accountService = accountService;
    }

    public override IActionResult Create()
    {
        var accounts = _accountService.GetAll();
        ViewBag.Accounts = new SelectList(accounts, nameof(Account.Id), nameof(Account.Number));

        return View();
    }
}