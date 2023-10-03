using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDeposits.Mvc.Controllers;

public abstract class BaseController<TEntity, TService> : Controller where TEntity : IdentifierEntity
    where TService : BaseService<TEntity>
{
    protected BaseController(TService service) => Service = service;

    private TService Service { get; }

    public async virtual Task<IActionResult> Index() => View(await Service.GetAllAsync());

    public async virtual Task<IActionResult> Edit(Guid id)
    {
        var entity = await Service.GetAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async virtual Task<IActionResult> Edit(Guid id, TEntity entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        var updatedEntity = await Service.UpdateAsync(entity);
        if (updatedEntity is null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    public async virtual Task<IActionResult> Details(Guid id)
    {
        var entity = await Service.GetAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        return View(entity);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(entity);
        }

        await Service.CreateAsync(entity);
        return RedirectToAction(nameof(Index));
    }
}