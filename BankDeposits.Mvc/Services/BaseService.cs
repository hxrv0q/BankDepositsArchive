using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public abstract class BaseService<TEntity> where TEntity : IdentifierEntity
{
    protected BaseService(BankDepositsContext context) => Context = context;

    public readonly BankDepositsContext Context;

    public async virtual Task<List<TEntity>> GetAllAsync() => await Context.Set<TEntity>().ToListAsync();

    public async virtual Task<TEntity?> GetAsync(Guid? id) => await Context.Set<TEntity>().FindAsync(id);

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

    public async virtual Task<TEntity> CreateAsync(TEntity entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public async virtual Task DeleteAsync(Guid id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);
        if (entity is null)
        {
            return;
        }

        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    private bool EntityExists(Guid id) => Context.Set<TEntity>().Any(e => e.Id == id);
}