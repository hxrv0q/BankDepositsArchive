using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankDeposits.Mvc.Controllers;

public class AccountController : BaseController<Account, AccountService>
{
    private readonly DepositorService _depositorService;

    public AccountController(AccountService service, DepositorService depositorService) : base(service)
    {
        _depositorService = depositorService;
    }

    public override IActionResult Create()
    {
        var depositors = _depositorService.GetAll();
        ViewBag.Depositors = new SelectList(depositors, nameof(Depositor.Id), nameof(Depositor.Id));

        return View();
    }
}