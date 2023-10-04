using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public abstract class BaseService<TEntity> where TEntity : IdentifierEntity
{
    private readonly BankDepositsContext _context;

    protected BaseService(BankDepositsContext context) => _context = context;

    public virtual List<TEntity> GetAll() => _context.Set<TEntity>().ToList();

    public async virtual Task<List<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

    public async virtual Task<TEntity?> GetAsync(Guid? id) => await _context.Set<TEntity>().FindAsync(id);

    public async virtual Task<TEntity?> UpdateAsync(TEntity entity)
    {
        if (!EntityExists(entity.Id))
        {
            return null;
        }

        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async virtual Task<TEntity> CreateAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async virtual Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity is null)
        {
            return;
        }

        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    private bool EntityExists(Guid id) => _context.Set<TEntity>().Any(e => e.Id == id);
}