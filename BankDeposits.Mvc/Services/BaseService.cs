using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public abstract class BaseService<TEntity> where TEntity : IdentifierEntity
{
    protected BaseService(BankDepositsContext context) => Context = context;

    protected readonly BankDepositsContext Context;

    protected abstract DbSet<TEntity> Entities { get; }

    public async virtual Task<List<TEntity>> GetAllAsync() => await Entities.ToListAsync();

    public async virtual Task<TEntity?> GetAsync(Guid? id) => await Entities.FindAsync(id);

    public async virtual Task<TEntity?> UpdateAsync(TEntity entity)
    {
        if (!EntityExists(entity.Id))
        {
            return null;
        }

        Context.Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    private bool EntityExists(Guid id) => Entities.Any(e => e.Id == id);
}