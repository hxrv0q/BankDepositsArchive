using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDeposits.Mvc.Controllers;

public abstract class BaseController<TEntity, TService> : Controller where TEntity : IdentifierEntity
    where TService : BaseService<TEntity>
{
    private readonly TService _service;

    protected BaseController(TService service) => _service = service;

    public async virtual Task<IActionResult> Index() => View(await _service.GetAllAsync());

    public async virtual Task<IActionResult> Edit(Guid id)
    {
        var entity = await _service.GetAsync(id);
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

        var updatedEntity = await _service.UpdateAsync(entity);
        if (updatedEntity is null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    public async virtual Task<IActionResult> Details(Guid id)
    {
        var entity = await _service.GetAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        return View(entity);
    }

    public virtual IActionResult Create() => View();

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

        await _service.CreateAsync(entity);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id) => View(await _service.GetAsync(id));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}