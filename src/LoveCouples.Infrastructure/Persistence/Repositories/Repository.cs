using System.Linq.Expressions;
using LoveCouples.Domain.Contracts;
using LoveCouples.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoveCouples.Infrastructure.Persistence.Repositories;

public abstract class Repository<TEntity, TContext>(TContext dbContext) : IRepository<TEntity>
    where TEntity : Entity
    where TContext : CoreDbContext
{
    public IUnitOfWork UnitOfWork => dbContext;
    public IQueryable<TEntity> QueryableSet => dbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity, CancellationToken ctk = default)
    {
        await dbContext.AddAsync(entity, ctk);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken ctk = default)
    {
        dbContext.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken ctk = default)
    {
        dbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> AnyAsync(Guid id, CancellationToken ctk = default)
    {
        return QueryableSet.AnyAsync(x => x.Id == id, ctk);
    }

    public Task<bool> AnyAsync(IQueryable<TEntity> query, CancellationToken ctk = default)
    {
        return query.AnyAsync(ctk);
    }

    public Task<TEntity> FirstAsync(IQueryable<TEntity> query, Guid id, CancellationToken ctk = default)
    {
        return query.FirstAsync(x => x.Id == id, ctk);
    }

    public Task<TEntity> FirstAsync(IQueryable<TEntity> query, CancellationToken ctk = default)
    {
        return query.FirstAsync(ctk);
    }

    public Task<TEntity?> FirstOrDefaultAsync(IQueryable<TEntity> query, Guid id, CancellationToken ctk = default)
    {
        return query.FirstOrDefaultAsync(x => x.Id == id, ctk);
    }

    public Task<TEntity?> FirstOrDefaultAsync(IQueryable<TEntity> query, CancellationToken ctk = default)
    {
        return query.FirstOrDefaultAsync(ctk);
    }

    public Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken ctk = default)
    {
        return query.ToListAsync(ctk);
    }

    public Task<List<T>> SelectToListAsync<T>(IQueryable<TEntity> query, Expression<Func<TEntity, T>> select,
        CancellationToken ctk = default)
    {
        return query.Select(select).ToListAsync(ctk);
    }
}