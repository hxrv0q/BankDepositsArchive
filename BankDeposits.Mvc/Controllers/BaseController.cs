using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Controllers;

public abstract class BaseController<TEntity, TService> : Controller where TEntity : IdentifierEntity
    where TService : BaseService<TEntity>
{
    private readonly TService _service;

    protected BaseController(TService service) => _service = service;

    public async virtual Task<IActionResult> Index() => View(await _service.GetAllAsync());

    public async virtual Task<IActionResult> Edit(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _service.GetAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        return View(entity);
    }
}