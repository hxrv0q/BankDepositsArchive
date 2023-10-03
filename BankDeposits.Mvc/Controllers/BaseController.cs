using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDeposits.Mvc.Controllers;

public abstract class BaseController<TEntity, TService> : Controller where TEntity : IdentifierEntity
    where TService : BaseService<TEntity>
{
    protected BaseController(TService service) => Service = service;

    protected TService Service { get; }

    public async virtual Task<IActionResult> Index() => View(await Service.GetAllAsync());

}