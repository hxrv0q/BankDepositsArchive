using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public abstract class BaseService<TEntity> where TEntity : IdentifierEntity
{
    protected BaseService(BankDepositsContext context) => Context = context;

    protected readonly BankDepositsContext Context;

    protected abstract DbSet<TEntity> Entities { get; }

    public async Task<List<TEntity>> GetAllAsync() => await Entities.ToListAsync();

    public async Task<TEntity?> GetAsync(Guid? id) => await Entities.FindAsync(id);

    protected bool EntityExists(Guid id) => Entities.Any(e => e.Id == id);
}