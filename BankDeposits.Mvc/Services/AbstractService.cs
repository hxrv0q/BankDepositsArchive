using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public abstract class AbstractService<TEntity>
    where TEntity : IdentifierEntity
{
    private readonly BankDepositsContext _context;

    protected AbstractService(BankDepositsContext context) => _context = context;

    public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();

    public async virtual Task<List<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

    public async virtual Task<TEntity?> GetAsync(Guid? id) => await _context.Set<TEntity>().FindAsync(id);

    public async virtual Task<TEntity?> AddOrUpdateAsync(TEntity entity)
    {
        if (entity.Id == Guid.Empty)
        {
            _context.Add(entity);
        }
        else
        {
            _context.Update(entity);
        }

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
}