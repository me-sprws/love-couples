using LoveCouples.Domain.Contracts;
using LoveCouples.Domain.Repositories;
using LoveCouples.Domain.Services;
using LoveCouples.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoveCouples.Infrastructure.Persistence;

public abstract class CoreDbContext(
    DbContextOptions options, 
    IDateTimeProvider dateTimeProvider
) : DbContext(options), IUnitOfWork
{
    // TODO: refactor this shit
    IDbContextTransaction? _transaction;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor(dateTimeProvider));
        base.OnConfiguring(optionsBuilder);
    }

    public async Task<IDisposable> BeginTransactionAsync(CancellationToken ctk = default)
    {
        if (_transaction is not null)
            throw new InvalidOperationException("You can have only one active transaction");

        return _transaction = await Database.BeginTransactionAsync(ctk);
    }

    public Task CommitTransactionAsync(CancellationToken ctk = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException("You must call BeginTransactionAsync() before CommitTransaction()");

        return _transaction.CommitAsync(ctk);
    }
}